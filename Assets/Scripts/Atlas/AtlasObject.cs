using Game.Interfaces;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Atlas.Data
{
    [System.Serializable]
    abstract public class AtlasObject : ScriptableObject, IInsertable
    {
        [SerializeField] protected string oName; //Don't use name cause SO already have this field
        [TextArea(3, 10)] [SerializeField] protected string description;
        [SerializeField] protected Sprite photo;
        [HideInInspector] abstract public AtlasCategory Category { get; }
        protected bool isOpened = false;
        

        public void Open()
        {
            isOpened = true;
        }
        public virtual void InsertInInterface(Transform i)
        {
            Text nameField = i.Find(UILabels.NAME).GetComponentInChildren<Text>();
            Text descriptionField = i.Find(UILabels.DESCRIPTION).GetComponentInChildren<Text>();
            InsertIfOpened(nameField, oName);
            InsertIfOpened(descriptionField, description);
        }

        public bool IsOpened()
        {
            return isOpened;
        }

        public string GetName()
        {
            return oName;
        }

        protected virtual void InsertIfOpened(Text textField, string content)
        {
            textField.text = isOpened ? content : "?";
        }
    }
}