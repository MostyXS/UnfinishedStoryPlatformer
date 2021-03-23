using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

namespace Game.Collectioning
{
    public class AtlasCategory : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private AtlasCategoryType categoryType;
        [SerializeField] private List<AtlasObject> atlasObjects = new List<AtlasObject>();

        public AtlasCategoryType GetCategoryType()
        {
            return categoryType;
        }

        public IEnumerable<AtlasObject> GetAllObjects()
        {
            return atlasObjects;
        }

        public void LoadFromOther(AtlasCategory other)
        {
            foreach (var atlasObject in GetAllObjects())
            {
                foreach (var otherAtlasObject in other.GetAllObjects())
                {
                    if(atlasObject.name == otherAtlasObject.name && otherAtlasObject.IsOpened())
                        atlasObject.Open();
                    
                }
                
            }
        }
        
#if UNITY_EDITOR
        public AtlasObject AddObject()
        {
            Undo.RecordObject(this, "Atlas Object Added");
            var newObject = CreateInstance<AtlasObject>();
            newObject.name = Guid.NewGuid().ToString();
            newObject.SetCategoryType(this.categoryType);
            atlasObjects.Add(newObject);
            Undo.RegisterCreatedObjectUndo(newObject, "Atlas Object Created");
            return newObject;
        }

        public void SetCategoryType(AtlasCategoryType newType)
        {
            Undo.RecordObject(this, "Category Type Change");
            categoryType = newType;
            foreach (AtlasObject o in GetAllObjects())
            {
                o.SetCategoryType(newType);
            }

        }

        public void RemoveObject(AtlasObject objectToRemove)
        {
            Undo.RecordObject(this, "Atlas Object Remove");
            atlasObjects.Remove(objectToRemove);
            Undo.DestroyObjectImmediate(objectToRemove);
        }

#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (atlasObjects.Count == 0) return;
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this)))
            {
                foreach (AtlasObject aObject in GetAllObjects())
                {
                    if (aObject == null) continue;
                    if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(aObject)))
                    {
                        AssetDatabase.AddObjectToAsset(aObject, this);
                        AssetDatabase.SaveAssets();
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}