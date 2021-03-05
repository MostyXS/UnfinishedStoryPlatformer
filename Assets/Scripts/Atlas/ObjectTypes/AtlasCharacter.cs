using UnityEngine;

namespace Game.Atlas.Data
{
    [CreateAssetMenu(fileName = "Atlas Character", menuName = "Game/Atlas/Character")]
    public class AtlasCharacter : AtlasObject
    {

        public override AtlasCategory Category { get { return AtlasCategory.Character; } }


    }
}