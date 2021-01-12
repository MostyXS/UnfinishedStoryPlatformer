using System;
using UnityEngine;

namespace Game.Atlas.Data
{
    [Serializable]
    public class AtlasObjectsContainer : MonoBehaviour
    {
        [SerializeField] AtlasCategory category;
        [SerializeField] AtlasObject[] aObjects;

        public AtlasCategory GetCategory()
        {
            return category;
        }

        public AtlasObject[] GetArray()
        {
            return aObjects;
        }

    }
}