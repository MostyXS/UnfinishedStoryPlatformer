using Game.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Collectioning.UI
{
    public class AtlasUI : MonoBehaviour
    {
        [SerializeField] private Atlas atlas;

        [Header("Objects List")] [Tooltip("Requires Button")] [SerializeField]
        private GameObject atlasObjectButtonPrefab;

        [SerializeField] private Transform objectsSelector;
        [SerializeField] private AtlasObjectUI objectUI;

        [Header("UI")] [SerializeField] private GameObject categories;
        [SerializeField] private GameObject objects;


        private AtlasObject _lastOpenedObject;


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

            FillObjectSelectionList(category);
            if (_lastOpenedObject != null)
                objectUI.InsertAtlasObject(_lastOpenedObject);
        }

        private void FillObjectSelectionList(AtlasCategoryType category)
        {
            foreach (var aObj in atlas.GetCategoryByType(category).GetAllObjects())
            {
                var tempListObj = Instantiate(atlasObjectButtonPrefab, objectsSelector);
                var title = aObj.IsOpened() ? aObj.GetTitle() : "Hidden";
                tempListObj.GetComponentInChildren<TextMeshProUGUI>().text = title;
                if (aObj != null)
                {
                    tempListObj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        objectUI.InsertAtlasObject(aObj);
                        _lastOpenedObject = aObj;
                    });
                }
            }
        }

        #endregion
    }
}