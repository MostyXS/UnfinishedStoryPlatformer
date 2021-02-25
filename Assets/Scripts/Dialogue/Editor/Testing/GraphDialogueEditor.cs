/*using Game.Dialogues;
using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEngine;

public class GraphDialogueEditor : GraphViewEditorWindow
{
    private static Dialogue selectedDialogue;
    private Graph graph;
    private GraphGUITest graphGUI;

    private const float TOOLBAR_SIZE = 75f;

    class GraphGUITest : GraphGUI
    {
        public override void AddTools()
        {
           // GUILayout.Button("Zalupa");
           //
        }
    }

    #region Unity Methods
    private void OnEnable()
    {
        graph = CreateInstance<Graph>();
        graphGUI = CreateInstance<GraphGUITest>();
        graphGUI.graph = graph;
        Selection.selectionChanged += OnSelectionChanged;
    }
    private void OnGUI()
    {

        graphGUI.BeginToolbarGUI(new Rect(0, 0, position.width, TOOLBAR_SIZE));
        GUILayout.Button("Zalupa");
        graphGUI.EndToolbarGUI();
        graphGUI.BeginGraphGUI(this, new Rect(0, TOOLBAR_SIZE, position.width, position.height));

        graphGUI.EndGraphGUI();
        
    }
    #endregion

    #region Callbacks
    [MenuItem("Window/Graph Dialogue Editor")]
    public static void ShowEditorWindow()
    {
        GetWindow<GraphDialogueEditor>("Graph Dialogue Editor");
    }

    [OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        var dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if(dialogue == null)
        {
            EditorGUILayout.LabelField("No Dialogue Selected");
        }
        else
        {
            if (dialogue != selectedDialogue)
                selectedDialogue = dialogue;
            ShowEditorWindow();
            return true;
        }
        return false;
        
        
    }
    private void OnSelectionChanged()
    {
        Dialogue newDialogue = Selection.activeObject as Dialogue;
        if(newDialogue != null && newDialogue != selectedDialogue)
        {
            selectedDialogue = newDialogue;
            Repaint();

        }
    }
    #endregion

}
*/