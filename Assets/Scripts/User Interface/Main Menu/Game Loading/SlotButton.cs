using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using Game.Utils;
using UnityEngine;

namespace Game.UI
{
    public class SlotButton : MonoBehaviour
    {
        [Range(1, 5)]
        [SerializeField]
        protected int slotNumber = 1;

        protected bool IsSaveSlotExists()
        {
            //May be do style for each
            return SaveManager.IsSaveExists(slotNumber);
        }
    }
}