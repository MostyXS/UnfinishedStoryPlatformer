using Game.Collectioning;
using UnityEngine;

namespace Game.Menu.UI
{
    public class AtlasOpenButton : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(AtlasSaver.IsSaveExists());
        }
    }
}