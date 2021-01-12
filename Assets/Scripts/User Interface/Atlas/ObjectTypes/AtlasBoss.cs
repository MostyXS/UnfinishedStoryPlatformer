using UnityEngine;

namespace Game.Atlas.Data
{
    [CreateAssetMenu(fileName = "Atlas Boss", menuName = "Game/Atlas/Boss")]
    public class AtlasBoss : AtlasObject
    {

        public override AtlasCategory Category { get { return AtlasCategory.Boss; } }


    }
}
