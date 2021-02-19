using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TransformChooseButon : MonoBehaviour
{
    [SerializeField] Transform objectsList;
    [SerializeField] GameObject objectToActivate;

    Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(delegate { ButtonEvent(); });
    }

    private void ButtonEvent()
    {
        foreach (Transform c in objectsList)
        {
            c.gameObject.SetActive(c.gameObject == objectToActivate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
