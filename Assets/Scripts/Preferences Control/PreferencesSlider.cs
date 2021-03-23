using UnityEngine;
using UnityEngine.UI;


namespace Game.PreferencesControl
{
    [System.Serializable]
    public class PreferencesSlider
    {
        public string name;
        public PrefKey prefKey;
        public Slider slider;
        [Range(0, 1f)] public float defaultValue = .5f;

        public PreferencesSlider(Slider slider)
        {
            this.slider = slider;
        }

        public void SetPrefValue(float value)
        {
            PrefsController.SetClampedFloatWithKey(prefKey, value);
        }
    }
}