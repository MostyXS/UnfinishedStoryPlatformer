using System.Collections.Generic;
using UnityEngine;


namespace MostyProUI.PreferencesControl
{
    public static class PrefsController
    {
        private static float MasterVolume => GetClampedFloatByKey(PrefKey.MasterVolume);
        public static float EnvironmentVolume => GetVolumeByKey(PrefKey.EnvironmentVolume);
        public static float MusicVolume => GetVolumeByKey(PrefKey.MusicVolume);
        public static float CharactersVolume => GetVolumeByKey(PrefKey.CharactersVolume);
        public static float ZoomSensitivity => GetClampedFloatByKey(PrefKey.ZoomSensitivity);

        public static void SetClampedFloatWithKey(PrefKey key, float value)
        {
            PlayerPrefs.SetFloat(key.ToString(), Mathf.Clamp(value, 0, 1));
        }

        /// <summary>
        ///  returns raw float value clamped with 0 and 1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetClampedFloatByKey(PrefKey key)
        {
            return Mathf.Clamp(PlayerPrefs.GetFloat(key.ToString()), 0, 1);
        }

        public static bool HasKey(PrefKey prefKey)
        {
            return PlayerPrefs.HasKey(prefKey.ToString());
        }

        /// <summary>
        /// Returns volume value by key multiplied by master volume. Should not work with zoom sensivity
        /// and non-volume related
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static float GetVolumeByKey(PrefKey key)
        {
            if (!HasKey(key))
            {
                PlayerPrefs.SetFloat(key.ToString(), .5f);
            }

            return GetClampedFloatByKey(key) * MasterVolume;
        }
    }
}