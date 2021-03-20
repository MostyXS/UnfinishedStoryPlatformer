using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        //We should not have dynamic data the instantiates, for example if we wan't to have
        //dynamic enemies we should instantiate it from spawners or have it on scene already
        private SaveableEntity[] _saveableEntities;

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            UpdateSaveableList();
        }

        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int) state["lastSceneBuildIndex"];
            }

            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(state);
        }

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public static bool FileExists(string saveFile)
        {
            return File.Exists(GetPathFromSaveFile(saveFile));
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        public static void Delete(string saveFile)
        {
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>) formatter.Deserialize(stream);
            }
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
        {
            UpdateSaveableList();
        }

        private /*public*/ void UpdateSaveableList()
        {
            _saveableEntities = FindObjectsOfType<SaveableEntity>(true);
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in _saveableEntities)
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        /*private static bool IsSceneObject(SaveableEntity saveable)
        {
            return !(saveable.hideFlags == HideFlags.NotEditable || saveable.hideFlags == HideFlags.HideAndDontSave
#if UNITY_EDITOR
                                                                 || UnityEditor.EditorUtility.IsPersistent(
                                                                     saveable.transform.root.gameObject)
#endif
                );
        }*/

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in _saveableEntities)
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private static string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}