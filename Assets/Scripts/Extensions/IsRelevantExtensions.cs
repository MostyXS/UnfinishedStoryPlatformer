using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Extensions
{
    public static class IsRelevantExtensions
    {

        public static bool IsRelevant(this Decision decision)
        {
            return decision != null && GameManager.Instance.GetAllDecisions().Contains(decision);
        }
    }
}