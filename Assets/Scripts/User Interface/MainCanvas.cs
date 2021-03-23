using UnityEngine;


namespace Game.UI
{
    //Add to default scene Canvas(Defines menu and dead menu)
    public class MainCanvas : MonoBehaviour
    {
        public static Transform Instance { get; private set; }

        private void Awake()
        {
            Instance = transform;
        }
    }
}