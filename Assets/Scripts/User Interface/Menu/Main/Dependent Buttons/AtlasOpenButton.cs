using Game.Collectioning;
using UnityEngine;

namespace Game.UI.Menu
{
    public class AtlasOpenButton : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(AtlasSaver.IsSaveExists());
        }
    }
}