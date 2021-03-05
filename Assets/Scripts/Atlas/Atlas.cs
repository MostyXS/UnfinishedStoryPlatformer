using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atlas : MonoBehaviour
{
    

    private void Start() {
        if(!FileExists())
        {
            
        } 

    }

    public static bool FileExists()
    {

    }

    public static Dictionary<AtlasCategory, List<AtlasObject>> GetAtlasObjects()
    {
        if(!FileExists()) return null;
        
    }
    
}
