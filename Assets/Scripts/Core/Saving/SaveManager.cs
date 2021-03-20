using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Saving
{
    public class SaveManager : MonoBehaviour
    {
        private const string SAVE_PREFIX = "CyberSave_";
        private SavingSystem saveSys;

        private void Start() {
            saveSys = GetComponent<SavingSystem>();
        }
        public void Save()
        {
            saveSys.Save(GetCurrentSaveName());
        }

        public IEnumerator Load()
        {
            yield return saveSys.LoadLastScene(GetCurrentSaveName());
        }
        private static string GetCurrentSaveName()
        {
            return SAVE_PREFIX + PlayerPrefs.GetInt(PrefKey.CurrentSaveSlot.ToString());
        }
        public static void SetPreferredSlot(int slotNumber)
        {
            PlayerPrefs.SetInt(PrefKey.CurrentSaveSlot.ToString(), slotNumber);
        }

        public static bool IsSaveExists(int slotNumber)
        {
            return SavingSystem.FileExists(SAVE_PREFIX + slotNumber);
        }
    }
}