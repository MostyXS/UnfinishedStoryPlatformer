using JetBrains.Annotations;
using UnityEngine;


namespace Game.Core.Saving
{
    public class Checkpoint : MonoBehaviour
    {
        
        
        [UsedImplicitly]
        public void Save()
        {
            GameManager.Instance.Saver.Save();
        }
    }
}