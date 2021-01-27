using Game.Atlas.Data;
using Game.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Atlas.Main
{
    public class Atlas : MonoBehaviour
    {
        //Need remake cause changing interface

        [Header("Objects List")]
        [SerializeField] GameObject atlasContainers;
        [Tooltip("Requires Button")] [SerializeField] GameObject atlasListButtonPrefab;
        [SerializeField] Transform objectsListContent;
        [SerializeField] Transform objectInterface;

        [Header("UI")]
        [SerializeField] GameObject categories;
        [SerializeField] GameObject objectHUD;

        Dictionary<AtlasCategory, AtlasObject[]> atlasObjects = new Dictionary<AtlasCategory, AtlasObject[]>();
        Dictionary<AtlasCategory, AtlasObject> lastOpenedObjects = new Dictionary<AtlasCategory, AtlasObject>();
        bool isFilled = false;

        #region AtlasActivator Methods
        public void ActivateObject(AtlasObject o)
        {

        }
        #endregion
        #region Unity Methods
        private void Awake()
        {
            OpenAtlas();
        }
        private void Start()
        {
            FillAtlas();
        }
        #endregion
        #region UI Methods
        public void OpenAtlas()
        {
            /*if(!lastOpenedObject.Equals(null))
            {
                //Code
                return;
            }*/
            categories.SetActive(true);
            objectHUD.SetActive(false);


        }
        public void OpenCategory(int c)
        {
            var category = (AtlasCategory)c;

            categories.SetActive(false);
            objectHUD.SetActive(true);
            ClearChildren();
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
        #region Private Methods
        private void FillAtlas()
        {
            if (isFilled) return;
            foreach (var aObjCont in atlasContainers.GetComponents<AtlasObjectsContainer>())
            {
                atlasObjects.Add(aObjCont.GetCategory(), aObjCont.GetObjects());
            }
            isFilled = true;
        }
        #endregion

        /// <summary>
        /// 0 - Character
        /// 1 - Boss
        /// 2 - Enemy
        /// 3 - Newspaper
        /// 4 - Location
        /// </summary>
        /// <param name="category"></param>
        

        


        private void ClearChildren()
        {
            objectsListContent.Clear();
        }
        private void InsertObjIntoInterface(AtlasObject o)
        {
            lastOpenedObjects[o.Category] = o;
            o.InsertIntoInterface(objectInterface);
        }
        

    }
}