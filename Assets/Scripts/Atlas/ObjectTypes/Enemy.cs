using Game.Atlas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atlas Enemy", menuName = "Game/Atlas/Enemy")]
public class Enemy : AtlasObject
{

    public override AtlasCategory Category { get { return AtlasCategory.Enemy; } }
    
    
}
