using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.Collectioning
{
    public class AtlasSaver : MonoBehaviour
    {
        [SerializeField] Atlas atlas;

        private const string SaveName = "CyberAtlas";


        private void Awake()
        {
            AtlasObjectOpener.OnAtlasObjectOpen += OnAtlasObjectOpen;
            Load();
        }

        private void OnAtlasObjectOpen(AtlasObject aObj)
        {
            Save();
        }

        //TODO
        private void Save()
        {
            Dictionary<AtlasCategoryType, Dictionary<string, bool>> atlasCategories =
                new Dictionary<AtlasCategoryType, Dictionary<string, bool>>();

            foreach (var category in atlas.GetAllCategories())
            {
                atlasCategories[category.GetCategoryType()] = new Dictionary<string, bool>();
                foreach (var atlasObject in category.GetAllObjects())
                {
                    atlasCategories[category.GetCategoryType()][atlasObject.name] = atlasObject.IsOpened();
                }
            }

            using (FileStream fs = File.Open(GetSavePath(), FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, atlasCategories);
            }
        }

        //TODO
        private void Load()
        {
            if (!IsSaveExists()) return;
            Dictionary<AtlasCategoryType, Dictionary<string, bool>> atlasCategories;
            using (FileStream fs = File.Open(GetSavePath(), FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                atlasCategories = bf.Deserialize(fs) as Dictionary<AtlasCategoryType, Dictionary<string, bool>>;
            }

            if (atlasCategories == null) return;
            foreach (var category in atlas.GetAllCategories())
            {
                foreach (var atlasObject in category.GetAllObjects())
                {
                    if (atlasCategories[category.GetCategoryType()][atlasObject.name])
                        atlasObject.Open();
                }
            }
        }

        private static string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveName + ".asav");
        }

        public static bool IsSaveExists()
        {
            return File.Exists(GetSavePath());
        }
    }
}