using UnityEditor;
using UnityEngine;

namespace Game.Collectioning
{
    [System.Serializable]
    public class AtlasObject : ScriptableObject
    {
        [SerializeField] protected string title;
        [SerializeField] protected Sprite image;//Don't use name cause SO already have this field
        [TextArea(3, 10)] [SerializeField] protected string description;
        [SerializeField] protected AtlasCategoryType categoryType;
        protected bool isOpened = false;
        public void Open()
        {
            isOpened = true;
        }
        public bool IsOpened()
        {
            return isOpened;
        }

        public string GetTitle()
        {
            return title;
        }
        public string GetDescription()
        {
            return description;
        }
        public Sprite GetImage()
        {
            return image;
        }
        public AtlasCategoryType GetCategoryType()
        {
            return categoryType;
        }

#if UNITY_EDITOR

        public void SetImage(Sprite newImage)
        {
            Undo.RecordObject(this, "Object Image Change");
            this.image = newImage;
            EditorUtility.SetDirty(this);
        }
        public void SetCategoryType(AtlasCategoryType newType)
        {
            Undo.RecordObject(this, "Object Type Change");
            this.categoryType = newType;
            EditorUtility.SetDirty(this);

        }

        public void SetTitle(string newTitle)
        {
            Undo.RecordObject(this, "Object Title Change");
            this.title = newTitle;
            EditorUtility.SetDirty(this);
        }

        public void SetDescription(string newDesc)
        {
            Undo.RecordObject(this, "Object Description Change");
            this.description = newDesc;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}