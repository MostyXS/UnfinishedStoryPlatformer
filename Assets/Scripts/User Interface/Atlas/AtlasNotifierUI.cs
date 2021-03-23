using System.Collections;
using System.Collections.Generic;
using Game.Collectioning;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtlasNotifierUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectName;
    [SerializeField] private Image image;
    [SerializeField] private float showTime = 3f;

    void Start()
    {
        AtlasObjectOpener.OnAtlasObjectOpen += NotifyAboutOpen;
        gameObject.SetActive(false);
    }


    private void NotifyAboutOpen(AtlasObject obj)
    {
        objectName.text = obj.GetTitle();
        if (obj.GetImage() != null)
            image.sprite = obj.GetImage();

        gameObject.SetActive(true);
        StartCoroutine(ActivateNotification());
    }

    private IEnumerator ActivateNotification()
    {
        yield return new WaitForSeconds(showTime);
        gameObject.SetActive(false);
    }
}