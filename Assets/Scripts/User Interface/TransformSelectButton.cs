using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TransformSelectButton : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;

    Button _thisButton;

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(delegate { ButtonEvent(); });
    }

    private void ButtonEvent()
    {
        foreach (Transform c in objectToActivate.transform.parent)
        {
            c.gameObject.SetActive(c.gameObject == objectToActivate);
        }
    }
}
