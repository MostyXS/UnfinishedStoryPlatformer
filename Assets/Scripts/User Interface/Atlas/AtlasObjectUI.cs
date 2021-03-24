using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Collectioning.UI
{
    public class AtlasObjectUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] TextMeshProUGUI nameField;
        [SerializeField] TextMeshProUGUI descField;


        public void InsertAtlasObject(AtlasObject objectToInsert)
        {
            nameField.text = objectToInsert.GetTitle();
            descField.text = objectToInsert.GetDescription();
            image.sprite = objectToInsert.GetImage();
        }
    }

}