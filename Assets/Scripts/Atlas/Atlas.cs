using Game.Atlas.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Atlas.Main
{
    public class Atlas : MonoBehaviour
    {

        [SerializeField] GameObject categories;
        [Header("Objects List")]
        [SerializeField] GameObject atlasContainers;
        [SerializeField] GameObject objectsList;
        [SerializeField] Transform objectsListContent;
        [Tooltip("Requires Button")] [SerializeField] GameObject atlasListButtonPrefab;
        [Header("Object HUD")]
        [SerializeField] GameObject objectHUD;

        Dictionary<AtlasCategory, AtlasObject[]> atlasObjects = new Dictionary<AtlasCategory, AtlasObject[]>();
        KeyValuePair<AtlasCategory, AtlasObject> lastOpenedObject;
        bool isFilled = false;

        private void Start()
        {
            FillAtlas();
        }

        public void OpenAtlas()
        {
            /*if(!lastOpenedObject.Equals(null))
            {
                //Code
                return;
            }*/
            categories.SetActive(true);
            objectsList.gameObject.SetActive(false);
            objectHUD.SetActive(false);


        }

        private void FillAtlas()
        {
            if (isFilled) return;
            foreach (var aObjCont in atlasContainers.GetComponents<AtlasObjectsContainer>())
            {
                atlasObjects.Add(aObjCont.GetCategory(), aObjCont.GetArray());
            }
            isFilled = true;
        }

        /// <summary>
        /// 0 - Character
        /// 1 - Boss
        /// 2 - Enemy
        /// 3 - Newspaper
        /// 4 - Location
        /// </summary>
        /// <param name="category"></param>
        public void OpenCategory(int c)
        {
            var category = (AtlasCategory)c;


            categories.SetActive(false);
            objectsList.SetActive(true);
            ClearChildren();
            foreach (var o in atlasObjects[category])
            {
                var tempListObj = Instantiate(atlasListButtonPrefab, objectsListContent);
                var t = o != null ? o.GetName() : "?";
                tempListObj.GetComponentInChildren<Text>().text = t;
                if (o != null)
                {
                    tempListObj.GetComponent<Button>().onClick.AddListener(delegate { InsertInObjectHUD(o); });

                }
            }


        }

        public void InsertInObjectHUD(AtlasObject o)
        {
            objectHUD.SetActive(true);
            o.InsertInInterface(objectHUD.transform);
        }


        private void ClearChildren()
        {
            foreach (Transform child in objectsListContent)
            {
                Destroy(child.gameObject);
            }
        }

        public void ActivateObject(AtlasObject o)
        {

        }

    }
}