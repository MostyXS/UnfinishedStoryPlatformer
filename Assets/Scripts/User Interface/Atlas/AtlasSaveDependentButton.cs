using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Collectioning.UI
{
    public class AtlasSaveDependentButton : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(AtlasSaver.IsSaveExists());
        }

    }
}