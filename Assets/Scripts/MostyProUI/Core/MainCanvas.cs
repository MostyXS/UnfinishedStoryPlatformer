using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MostyProUI
{
    //Add to default scene Canvas(Defines menu and dead menu)
    public class MainCanvas : MonoBehaviour
    {
        public static Transform Instance
        {
            get; private set;
        }
        private void Awake()
        {
            Instance = transform;
        }
        
        


    }
}