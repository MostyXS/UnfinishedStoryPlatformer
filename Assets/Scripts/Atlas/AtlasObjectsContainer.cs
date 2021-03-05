using System;
using UnityEngine;

namespace Game.Atlas
{
    [Serializable]
    public class AtlasObjectsContainer : MonoBehaviour
    {
        [SerializeField] AtlasCategory category;
        [SerializeField] AtlasObject[] aObjects;

        #region Getters
        public AtlasCategory GetCategory()
        {
            return category;
        }

        public AtlasObject[] GetObjects()
        {
            return aObjects;
        }
        #endregion
    }
}