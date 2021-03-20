using UnityEngine;


namespace Game.Utils.Extensions
{
    public static class TransformExtensions
    {

        public static void Clear(this Transform t)
        {
            foreach(Transform c in t)
            {
                Object.Destroy(c.gameObject);
            }
        }

    }
}
