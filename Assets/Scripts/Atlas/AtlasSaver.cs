using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.Collectioning
{
    public class AtlasSaver : MonoBehaviour
    {
        [SerializeField] Atlas atlas;
        const string SAVE_NAME = "CyberAtlas";

        public void Save()
        {
            using (FileStream fs = File.Open(GetSavePath(), FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, atlas.GetAllCategories());
            }

        }
        public void Load()
        {
            if (!IsSaveExists()) return;

            using (FileStream fs = File.Open(GetSavePath(), FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                atlas.LoadCategories((List<AtlasCategory>)bf.Deserialize(fs));
            }
        }
        private static string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, SAVE_NAME + ".asav");
        }
        public static bool IsSaveExists()
        {
            return File.Exists(GetSavePath());
        }
    }
}