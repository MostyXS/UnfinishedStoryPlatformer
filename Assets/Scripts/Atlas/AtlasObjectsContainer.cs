using Game.Atlas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
