using Game.Atlas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atlas Newspaper", menuName = "Game/Atlas/Newspaper")]
public class Newspaper : AtlasObject
{

    public override AtlasCategory Category { get { return AtlasCategory.Newspaper; } }
    
    
}
