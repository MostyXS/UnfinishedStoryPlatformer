using System;
using System.Collections.Generic;
using System.Linq;
using Game.Utils.Extensions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Game.Collectioning
{
    [CreateAssetMenu(fileName = "Atlas", menuName = "Atlas/Atlas", order = 0)]
    public class Atlas : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<AtlasCategory> categories = new List<AtlasCategory>();
#if UNITY_EDITOR
        [SerializeField] private AtlasObjectOpener atlasOpenerPrefab;
#endif
        
        [UsedImplicitly]
        public void OpenObject(AtlasObject objectToOpen)
        {
            var category = GetCategoryByType(objectToOpen.GetCategoryType());
            if (category == null) return;

            foreach (var aObj in category.GetAllObjects())
            {
                if (aObj == objectToOpen)
                {
                    aObj.Open();
                }
            }
        }

        public void LoadCategories(List<AtlasCategory> otherCategories)
        {
            this.categories = otherCategories;
        }

        public List<AtlasCategory> GetAllCategories()
        {
            return categories;
        }


        public AtlasCategory GetCategoryByType(AtlasCategoryType type)
        {
            return categories.FirstOrDefault(c => c.GetCategoryType() == type);
        }


        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR


            if (string.IsNullOrEmpty(this.GetFolderPath())) return;

            var categoryTypes = Enum.GetValues(typeof(AtlasCategoryType));
            if(categoryTypes.Length == categories.Count) return;
            foreach (AtlasCategoryType categoryType in categoryTypes)
            {
                if (GetCategoryByType(categoryType) != null) continue;
                string categoryName = categoryType + " Atlas Category";
                var newAtlasCategory = CreateInstance<AtlasCategory>();
                newAtlasCategory.name = categoryName;
                newAtlasCategory.SetCategoryType(categoryType);
                categories.Add(newAtlasCategory);
                var categoriesPath = $"{this.GetFolderPath()}/Categories/{categoryName}.asset"; 
                AssetDatabase.CreateAsset(newAtlasCategory, categoriesPath);
                AssetDatabase.SaveAssets();
            }

            EditorUtility.SetDirty(this);

#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}