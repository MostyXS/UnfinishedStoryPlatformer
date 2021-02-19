using Game.Atlas.Data;
using Game.Extensions;
using MostyProUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Atlas.UI
{
    //Should be persistent in game and probably save on autosaves
    public class AtlasHUD : MonoBehaviour
    {
        [SerializeField] GameObject atlasContainers;
        [Header("Objects List")]
        [Tooltip("Requires Button")] [SerializeField] GameObject atlasListButtonPrefab;
        [SerializeField] Transform objectsListContent;
        [SerializeField] Transform objectInterface;

        [Header("UI")]
        [SerializeField] GameObject atlasTransform;
        [SerializeField] GameObject categories;
        [SerializeField] GameObject objectHUD;

        [HideInInspector] public UnityAction<AtlasObject> onObjectOpen { private get; set; }
        public static AtlasHUD Instance { get; private set; }

        Dictionary<AtlasCategory, AtlasObject[]> atlasObjects = new Dictionary<AtlasCategory, AtlasObject[]>();
        Dictionary<AtlasCategory, AtlasObject> lastOpenedObjects = new Dictionary<AtlasCategory, AtlasObject>();
        bool isCreated = false;

        #region Unity Methods
        private void Awake()
        {
            //OpenAtlas();
        }
        private void Start()
        {
            //When implemented save system should executes only on first game launch
            //CreateAtlas();
        }
        #endregion
        #region Public Methods
        public void OpenAtlasObject(AtlasObject o)
        {
            atlasObjects[o.Category].Where((x) => x.name == o.name).First().Open();
            onObjectOpen(o);
            Save();
        }

        #endregion
        #region UI Methods
        public void OpenAtlas()
        {
            categories.SetActive(true);
            objectHUD.SetActive(false);
        }
        /// <summary>
        /// 0 - Character
        /// 1 - Boss
        /// 2 - Enemy
        /// 3 - Newspaper
        /// 4 - Location
        /// </summary>
        /// <param name="c">category</param>
        public void OpenCategory(int c)
        {
            var category = (AtlasCategory)c;

            categories.SetActive(false);
            objectHUD.SetActive(true);
            objectsListContent.Clear();

            AtlasObject lastOpenedObject;
            if(lastOpenedObjects.TryGetValue(category, out lastOpenedObject))
            {
                lastOpenedObject.InsertIntoInterface(objectInterface);
            }
            foreach (var o in atlasObjects[category])
            {
                var tempListObj = Instantiate(atlasListButtonPrefab, objectsListContent);
                var t = o != null ? o.GetName() : "?";
                tempListObj.GetComponentInChildren<Text>().text = t;
                if (o != null)
                {
                    tempListObj.GetComponent<Button>().onClick.AddListener(delegate { InsertObjIntoInterface(o); });
                }
            }
        }

        #endregion

        #region File Methods
        public void CreateAtlas()
        {
            if (isCreated) return;
            foreach (var aObjCont in atlasContainers.GetComponents<AtlasObjectsContainer>())
            {
                atlasObjects.Add(aObjCont.GetCategory(), aObjCont.GetObjects());
            }
            isCreated = true;
            Save();
        }
        public void Load()
        {
            var dataPath = $"{Application.persistentDataPath}/atlas.save";
            Dictionary<string, object> state;
            using (var fs = File.Open(dataPath, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                state = (Dictionary<string, object>)bf.Deserialize(fs);

            }
            isCreated = (bool)state["isCreated"];
            atlasObjects = (Dictionary<AtlasCategory, AtlasObject[]>)state["objects"];
            lastOpenedObjects = (Dictionary<AtlasCategory, AtlasObject>)state["openedObjects"];
        }
        public void Save()
        {
            var dataPath = $"{Application.persistentDataPath}/atlas.save";
            Dictionary<string, object> state = new Dictionary<string, object>();
            state.Add("isCreated", isCreated);
            state.Add("objects", atlasObjects);
            state.Add("openedObjects", lastOpenedObjects);
            using (var fs = File.Open(dataPath, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, state);
            }
        }
        #endregion

        //???
        private void OnApplicationQuit()
        {
            Save();
        }

        //Button Event
        private void InsertObjIntoInterface(AtlasObject o)
        {
            o.InsertIntoInterface(objectInterface);
        }
    }
}