using Game.Atlas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atlas Character", menuName = "Game/Atlas/Character")]
public class Character : AtlasObject
{

    public override AtlasCategory Category { get { return AtlasCategory.Character; } }
    
    
}
