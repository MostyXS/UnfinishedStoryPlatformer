using Game.Atlas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Atlas.Data
{
    [CreateAssetMenu(fileName = "Atlas Newspaper", menuName = "Game/Atlas/Newspaper")]
    public class AtlasNewspaper : AtlasObject
    {

        public override AtlasCategory Category { get { return AtlasCategory.Newspaper; } }


    }
}