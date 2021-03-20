using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MostyProUI.PreferencesControl
{
    [ExecuteAlways]
    public class DefaultsController : MonoBehaviour
    {
        [Tooltip("Finds sliders in order of children")] [SerializeField]
        protected List<PreferencesSlider> optionSliders = new List<PreferencesSlider>();


        protected virtual void Start()
        {
            UpdateSliders();
        }

        private void UpdateSliders()
        {
            foreach (var prefSlider in optionSliders)
            {
                var slider = prefSlider.slider;
                slider.onValueChanged.AddListener(prefSlider.SetPrefValue);
                var key = prefSlider.prefKey;
                if (!PrefsController.HasKey(key))
                {
                    PrefsController.SetClampedFloatWithKey(key, prefSlider.defaultValue);
                }

                slider.value = PrefsController.GetClampedFloatByKey(key);
                slider.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = slider.name;
            }
        }


        #region Button Events

        [UsedImplicitly]
        public void SetDefaultSliderValues()
        {
            foreach (var prefSlider in optionSliders)
            {
                prefSlider.slider.value = prefSlider.defaultValue;
            }
        }

        [UsedImplicitly]
        public void SavePreferences()
        {
            PlayerPrefs.Save();
        }

        #endregion

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying) return;

            Slider[] sliders = GetComponentsInChildren<Slider>();
            WasteCheck(sliders);
            foreach (var s in optionSliders)
            {
                s.slider.transform.parent.gameObject.name = s.name;
                s.slider.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = s.name;
            }

            if (optionSliders.Count >= sliders.Length) return;
            CreateSliderLists(sliders);
        }

        private void WasteCheck(Slider[] sliders)
        {
            if (optionSliders.Count <= sliders.Length) return;

            optionSliders.Remove(optionSliders[optionSliders.Count - 1]);
        }

        private void CreateSliderLists(Slider[] sliders)
        {
            foreach (var slider in sliders)
            {
                if (!optionSliders.Contains(slider))
                {
                    optionSliders.Add(new PreferencesSlider(slider));
                }
            }
        }
#endif
    }
}