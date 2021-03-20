using Game.Core;
using UnityEngine;


namespace Game.Collectioning
{
    public class AtlasObjectOpener : MonoBehaviour
    {
        [SerializeField] private AtlasObject atlasObjectToOpen;

        public void OpenAtlasObject()
        {
            GameManager.Instance.Atlas.OpenObject(atlasObjectToOpen);
        }
    }
}