using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Atlas.UI
{
    public class AtlasObjectUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameField;
        [SerializeField] TextMeshProUGUI descField;


        public void InsertAtlasObject(AtlasObject objectToInsert)
        {
            nameField.text = objectToInsert.GetName();
            descField.text = objectToInsert.GetDescription();
        }
    }

}