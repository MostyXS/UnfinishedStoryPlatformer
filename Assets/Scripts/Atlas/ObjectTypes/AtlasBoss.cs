using Game.Atlas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atlas Boss", menuName = "Game/Atlas/Boss")]
public class AtlasBoss : AtlasObject
{

    public override AtlasCategory Category { get { return AtlasCategory.Boss; } }
    
    
}
