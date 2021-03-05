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
    public class AtlasUI : MonoBehaviour
    {
        [SerializeField] GameObject atlasContainers;

        [Header("Objects List")]
        [Tooltip("Requires Button")] [SerializeField] GameObject atlasListButtonPrefab;
        [SerializeField] Transform objectsList;
        [SerializeField] AtlasObjectUI objectUI;

        [Header("UI")]
        [SerializeField] GameObject categories;
        [SerializeField] GameObject objects;

        private Dictionary<AtlasCategory, List<AtlasObject>> atlasObjects = new Dictionary<AtlasCategory, List<AtlasObject>>();
        private AtlasObject lastOpenedObject;


        #region Unity Methods
        private void OnEnable()
        {
            ReloadAtlas();
        }

        private void ReloadAtlas()
        {
        }
        #endregion

        #region UI Methods

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
            objects.SetActive(true);
            objectsList.Clear();
            foreach (var o in atlasObjects[category])
            {
                var tempListObj = Instantiate(atlasListButtonPrefab, objectsList);
                var t = o != null ? o.GetName() : "?";
                tempListObj.GetComponentInChildren<Text>().text = t;
                if (o != null)
                {
                    tempListObj.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        objectUI.InsertAtlasObject(o);
                        lastOpenedObject = o;
                    });
                }
            }
        }

        #endregion

    }
}