using Game.Saving;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Utils;
using System;
using System.Collections;

public class SaveSlotButton : MonoBehaviour
{
    [Range(1,5)][SerializeField] int slotNumber = 1;
    bool isClicked;
    void Start()
    {
        var textField = GetComponentInChildren<TextMeshProUGUI>();
        textField.text = $"Slot {slotNumber}";
        if(IsSaveExists())
        {
            textField.text += "(Saved)";
        }
        GetComponent<Button>().onClick.AddListener(CreateNewSaveFile);
    }

    private void CreateNewSaveFile()
    {
        if(!IsSaveExists() || isClicked)
        {
            PlayerPrefs.SetInt(PrefKey.CurrentSaveSlot.ToString(), slotNumber);
            SceneManager.LoadScene("00_tutorial");
        }
        else
        {
            StartCoroutine(WaitForDoubleClick());
        }
    }

    private IEnumerator WaitForDoubleClick()
    {
        isClicked = true;
        var textField = GetComponentInChildren<TextMeshProUGUI>();
        textField.text = "Click to overwrite";
        float time = 0f;
        var newColor = textField.color;
        while(time < 1f)
        {
            newColor.a = 255 * time / 1f;
            textField.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }
        newColor.a = 255;
        textField.color = newColor;
        textField.text = $"Slot {slotNumber} \n (Saved)";
        isClicked = false;


    }


    private bool IsSaveExists()
    {
        //May be do style for each
        return SavingSystem.FileExists(Prefixes.SavePrefix + slotNumber);
    }

}
