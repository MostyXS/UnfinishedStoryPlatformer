using Game.Saving;
using JetBrains.Annotations;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    
    [UsedImplicitly]
    public void Save()
    {
        GameManager.Instance.Saver.Save();
    }


}