using UnityEngine;

namespace Game.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectsPrefab;
        private static bool _hasSpawned;

        private void Awake()
        {
            if (_hasSpawned) return;
            GameObject persistentObjects = Instantiate(persistentObjectsPrefab);
            DontDestroyOnLoad(persistentObjects);
            _hasSpawned = true;
        }
    }
}