using UnityEngine;

namespace Game.Atlas.Data
{
    [CreateAssetMenu(fileName = "Atlas Enemy", menuName = "Game/Atlas/Enemy")]
    public class AtlasEnemy : AtlasObject
    {

        public override AtlasCategory Category { get { return AtlasCategory.Enemy; } }


    }
}