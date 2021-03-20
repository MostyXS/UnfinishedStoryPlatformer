using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class PanelSelector : EditorWindow
{
    private Transform _activePanelsList;
    private Transform _currentActivePanel;

    [UsedImplicitly]
    [MenuItem("Game/Tools/Panel Selector")]
    public static void ShowWindow()
    {
        GetWindow<PanelSelector>("Panel Selector").Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Activate on selected panel"))
        {
            _activePanelsList = Selection.activeTransform;
            _currentActivePanel = null;
        }

        if (_activePanelsList == null) return;

        foreach (Transform child in _activePanelsList)
        {
            if (GUILayout.Button(child.name))
            {
                if (_currentActivePanel != null)
                    _currentActivePanel.gameObject.SetActive(false);
                _currentActivePanel = child;
                Selection.activeObject = _currentActivePanel.gameObject;
            }

            child.gameObject.SetActive(child == _currentActivePanel && _currentActivePanel != null);
        }
    }
}