using Game.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Collectioning.UI
{
    public class AtlasUI : MonoBehaviour
    {
        [SerializeField] private Atlas atlas;

        [Header("Objects List")] [Tooltip("Requires Button")] [SerializeField]
        GameObject atlasObjectButtonPrefab;

        [SerializeField] Transform objectsSelector;
        [SerializeField] AtlasObjectUI objectUI;

        [Header("UI")] [SerializeField] GameObject categories;
        [SerializeField] GameObject objects;


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
            var category = (AtlasCategoryType) c;

            categories.SetActive(false);
            objects.SetActive(true);
            objectsSelector.Clear();
            foreach (var o in atlas.GetCategoryByType(category).GetAllObjects())
            {
                var tempListObj = Instantiate(atlasObjectButtonPrefab, objectsSelector);
                var t = o != null ? o.GetTitle() : "?";
                tempListObj.GetComponentInChildren<Text>().text = t;
                if (o != null)
                {
                    tempListObj.GetComponent<Button>().onClick.AddListener(() =>
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