using MostyProUI.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MostyProUI.PrefsControl
{
    [ExecuteInEditMode]
    public class DefaultsController : MonoBehaviour
    {
        [Tooltip("Finds sliders in order of childs")]
        [SerializeField] List<PrefsSlider> optionSliders = new List<PrefsSlider>();
        VolumeController[] volumeChangers;

        private void Start()
        {
            CheckForVolumeChangers();
        }

        //Used for performance optimization to prevent calling on Update
        private void CheckForVolumeChangers()
        {
            volumeChangers = FindObjectsOfType<VolumeController>();
            for (int i = 0; i < optionSliders.Count; i++)
            {
                for (int j = 0; j < volumeChangers.Length; j++)
                {
                    optionSliders[i].slider.onValueChanged.AddListener(volumeChangers[j].OnVolumeChanged);
                }
            }
        }

        private void Awake()
        {
            SetSliders();
        }

        private void SetSliders()
        {
            for (int i = 0; i < optionSliders.Count; i++)
            {
                var optSlider = optionSliders[i];
                optSlider.slider.onValueChanged.AddListener(optionSliders[i].SetPrefValue);
                optSlider.slider.value = PrefsController.GetDefaultVolumeByKey(optionSliders[i].prefKey);
                optSlider.slider.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = optSlider.name;
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying) return;
            
            Slider[] sliders = GetComponentsInChildren<Slider>();
            WasteCheck(sliders);
            foreach(var s in optionSliders)
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

            for (int i = 0; i < sliders.Length; i++)
            {
                if (!optionSliders.Contains(sliders[i]))
                {
                    optionSliders.Add(new PrefsSlider(sliders[i]));
                }
            }
        }
#endif
        //Button Action
        public void SetDefaultSlidersValue()
        {
            for (int i = 0; i < optionSliders.Count; i++)
            {
                optionSliders[i].slider.value = optionSliders[i].defaultValue;
            }
        }
        public void SavePrefs()
        {
            PlayerPrefs.Save();
        }

      
    }
}
