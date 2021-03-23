using System;
using UnityEditor;
using UnityEngine;


namespace Game.Collectioning
{
    public class AtlasObjectOpener : MonoBehaviour
    {
        [SerializeField] private AtlasObject atlasObjectToOpen;
        public static event Action<AtlasObject> OnAtlasObjectOpen;
        public void OpenAtlasObject()
        {
            Debug.Log(atlasObjectToOpen.IsOpened());
            if (!atlasObjectToOpen.IsOpened())
            {
                atlasObjectToOpen.Open();
                OnAtlasObjectOpen?.Invoke(atlasObjectToOpen);
            }
        }
#if UNITY_EDITOR
        public void SetObjectToOpen(AtlasObject obj)
        {
            Undo.RecordObject(this, "Changed Object To Open");
            atlasObjectToOpen = obj;
        }
#endif
    }
}