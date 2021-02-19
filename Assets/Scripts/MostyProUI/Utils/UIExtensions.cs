using MostyProUI.PrefsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MostyProUI.Utils
{
    public static class UIExtensions
    {
        public static bool Contains(this List<PrefsSlider> prefsSliders, Slider slider)
        {
            for (int i = 0; i<prefsSliders.Count; i++)
            {
                if (prefsSliders[i].slider != slider) continue;
                return true;
            }
            return false;
        }

        public static IEnumerator WaitForCurrentAnimation(this Animator animator)
        {
            yield return new WaitForSeconds(.05f);
            float timeToAwait = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(timeToAwait);
        }
    }
}
