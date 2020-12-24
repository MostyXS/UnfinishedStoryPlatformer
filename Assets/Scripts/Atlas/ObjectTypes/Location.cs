using Game.Atlas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atlas Location", menuName = "Game/Atlas/Location")]
public class Location : AtlasObject
{

    public override AtlasCategory Category { get { return AtlasCategory.Location; } }
    
    
}
