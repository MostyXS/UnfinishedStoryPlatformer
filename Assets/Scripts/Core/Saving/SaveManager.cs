using System.Collections;
using System.Collections.Generic;
using Game.PreferencesControl;
using UnityEngine;

namespace Game.Core.Saving
{
    public class SaveManager : MonoBehaviour
    {
        private const string SavePrefix = "CyberSave_";
        private SavingSystem _saveSystem;

        private void Start()
        {
            _saveSystem = GetComponent<SavingSystem>();
        }

        public void Save()
        {
            _saveSystem.Save(GetCurrentSaveName());
        }

        public IEnumerator Load()
        {
            yield return _saveSystem.LoadLastScene(GetCurrentSaveName());
        }

        private static string GetCurrentSaveName()
        {
            return SavePrefix + PlayerPrefs.GetInt(PrefKey.CurrentSaveSlot.ToString());
        }

        public static void SetPreferredSlot(int slotNumber)
        {
            PlayerPrefs.SetInt(PrefKey.CurrentSaveSlot.ToString(), slotNumber);
        }

        public static bool IsSaveExists(int slotNumber)
        {
            return SavingSystem.FileExists(SavePrefix + slotNumber);
        }
    }
}