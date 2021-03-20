using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MostyProUI.PreferencesControl;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class UtilityExtensions
{
    public static string GetFolderPath(this ScriptableObject so)
    {
        return AssetDatabase.GetAssetPath(so).Replace($"/{so.name}.asset", "");
    }

    public static bool Contains(this List<PreferencesSlider> prefsSliders, Slider slider)
    {
        return prefsSliders.Any(prefSlider => prefSlider.slider == slider);
    }

    public static IEnumerator WaitForCurrentAnimation(this Animator animator)
    {
        yield return new WaitForSeconds(.05f);
        float timeToAwait = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeToAwait);
    }
}