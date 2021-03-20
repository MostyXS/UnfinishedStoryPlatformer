using System.Collections;
using System.Collections.Generic;
using Game.Collectioning;
using UnityEngine;

public class AtlasObjectOpener : MonoBehaviour
{
    [SerializeField] private AtlasObject atlasObjectToOpen;

    public void OpenAtlasObject()
    {
        GameManager.Instance.Atlas.OpenObject(atlasObjectToOpen);
    }
}