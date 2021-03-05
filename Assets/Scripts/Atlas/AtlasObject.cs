using UnityEngine;

namespace Game.Atlas
{
    [System.Serializable]
    abstract public class AtlasObject : ScriptableObject
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
        public bool IsOpened()
        {
            return isOpened;
        }

        public string GetName()
        {
            return oName;
        }
        public string GetDescription()
        {
            return description;
        }
    }
}