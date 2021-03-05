using Game.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

[ExecuteInEditMode]
public class RebindContent : MonoBehaviour
{


    public void ResetAll()
    {
        foreach (Transform child in this.transform)
        {
            var rebindUI = child.GetComponent<RebindActionUI>();
            if (rebindUI != null)
            {
                rebindUI.ResetToDefault();
            }
            else
            {
                Debug.LogError("No rebind action UI on " + rebindUI.name);

            }
        }
    }
}
