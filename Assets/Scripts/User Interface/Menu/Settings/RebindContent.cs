using JetBrains.Annotations;
using UnityEngine;

namespace Game.Menu.UI
{
    public class RebindContent : MonoBehaviour
    {
        [UsedImplicitly]
        public void ResetAll()
        {
            foreach (Transform child in this.transform)
            {
                var rebindUi = child.GetComponent<RebindActionUi>();
                if (rebindUi != null)
                {
                    rebindUi.ResetToDefault();
                }
                else
                {
                    Debug.LogError("No rebind action UI on " + rebindUi.name);
                }
            }
        }
    }
}