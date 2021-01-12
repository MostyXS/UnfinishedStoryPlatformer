using UnityEngine;

namespace Game.Atlas.Data
{
    [CreateAssetMenu(fileName = "Atlas Location", menuName = "Game/Atlas/Location")]
    public class AtlasLocation : AtlasObject
    {

        public override AtlasCategory Category { get { return AtlasCategory.Location; } }


    }
}