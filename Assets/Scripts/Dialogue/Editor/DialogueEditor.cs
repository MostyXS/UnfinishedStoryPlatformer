using Game.Core;
using Game.Utils.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.Dialogues.Editor
{
    public class DialogueEditor : EditorWindow
    {
        [NonSerialized]
        private static Dialogue selectedDialogue;

        #region Styles Data
        [NonSerialized]
        private GUIStyle labelStyle;
        [NonSerialized]
        private GUIStyle conjunctionNodeStyle;
        [NonSerialized]
        private GUIStyle defaultDisjunctionNodeStyle;
        [NonSerialized]
        private GUIStyle negatedPredicateNodeStyle;
        [NonSerialized]
        private GUIStyle npcNodeStyle;
        [NonSerialized]
        private GUIStyle playerNodeStyle;
        [NonSerialized]
        private GUIStyle textAreaStyle;
        [NonSerialized]
        private GUIStyle textFieldStyle;
        [NonSerialized]
        private GUIStyle textStyle;
        [NonSerialized]
        private GUIStyle buttonStyle;
        #endregion

        #region Interaction Data
        [NonSerialized]
        private DialogueNode draggingNode = null;
        [NonSerialized]
        private DialogueNode creatingNode = null;
        [NonSerialized]
        private DialogueNode removingNode = null;
        [NonSerialized]
        private DialogueNode linkingParentNode = null;
        [NonSerialized]
        private DialogueNode conditionCopyNode = null;
        [NonSerialized]
        private bool draggingCanvas = false;
        [NonSerialized]
        private Condition.Disjunction disjToRemove;
        [NonSerialized]
        private Condition.Predicate conjToRemove;
        #endregion

        #region Const Data
        [NonSerialized]
        private const int DEFAULT_FONT_SIZE = 15;
        [NonSerialized]
        private const float BACKGROUND_SIZE = 120f;
        [NonSerialized]
        private const float ZOOM_SPEED = .01f;
        [NonSerialized]
        private const float MIN_ZOOM = .2f;
        [NonSerialized]
        private const float MAX_ZOOM = 5f;
        [NonSerialized]
        private const float CONDITION_SIZE = 250f;
        [NonSerialized]
        private const float MAX_CONDITIONAREA_HEIGHT = 600f; 
        [NonSerialized]
        private const float Y_CONDITION_OFFSET = -200;
        #endregion
        
        #region Zoom data
        [NonSerialized]
        private Vector2 zoomCoordsOrigin = Vector2.zero;
        [NonSerialized]
        private readonly Rect zoomArea = new Rect(0f, 0f, 8000f, 6000f);
        [NonSerialized]
        private float currentZoom = Mathf.Lerp(MIN_ZOOM, MAX_ZOOM, .1f);
        [NonSerialized]
        private Vector2 textureOffset = Vector2.zero;
        #endregion
        #region Unity Callbacks
        [MenuItem("Window/DialogueEditor")]
        public static void ShowEditorWindow()
        {
           GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }
        
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var d = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (d != null)
            {
                if(selectedDialogue != d)
                    selectedDialogue = d;
                ShowEditorWindow();
                return true; 
            }
            return false;
        }

        private void OnEnable()
        {
            if (selectedDialogue != null)
                zoomCoordsOrigin = selectedDialogue.GetRootNode().GetRect().position
                    - new Vector2(position.width / 2, position.height / 2);


            textStyle = new GUIStyle() { fontSize = DEFAULT_FONT_SIZE };
            npcNodeStyle = GetNodeStyle("node2");
            playerNodeStyle = GetNodeStyle("node3");

            negatedPredicateNodeStyle = GetConditionNodeStyle("node6");
            defaultDisjunctionNodeStyle = GetConditionNodeStyle("node4");
            conjunctionNodeStyle = GetConditionNodeStyle("node1");
            labelStyle = new GUIStyle { fontSize = 20, alignment = TextAnchor.UpperCenter };
            Selection.selectionChanged += OnSelectionChanged;
            OnSelectionChanged();

        }

        private void OnGUI()
        {
            SetButtonStyle();
            SetTextFieldStyle();

            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No dialogue selected");
            }
            else
            {
                ProcessEditorDraw();
            }

        }
        private void OnSelectionChanged()
        {
            var newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null && newDialogue != selectedDialogue)
            {
                selectedDialogue = newDialogue;
                zoomCoordsOrigin = selectedDialogue.GetRootNode().GetRect().position;
                Repaint();
            }
        }

        #endregion

        #region GUI Drawers
        private void ProcessEditorDraw()
        {
            HandleEvents();
            EditorZoomArea.Begin(currentZoom, zoomArea);

            DrawBackground();
            DrawNodesAndNodeConnections();
            TryRemoveNode();
            TryCreateNode();

            EditorZoomArea.End();
        }
        private void DrawNodesAndNodeConnections()
        {

            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawConnections(node);
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawNode(node);
                DrawCondition(node);
            }
        }
        
        private void DrawBackground()
        {
            var backgroundTex = Resources.Load("background") as Texture2D;
            Rect texCoords = new Rect(0, 0,
                zoomArea.width / BACKGROUND_SIZE,
                zoomArea.height / BACKGROUND_SIZE);
            Rect texPosition = new Rect(-BACKGROUND_SIZE + textureOffset.x,
                -BACKGROUND_SIZE + textureOffset.y,
                zoomArea.width, zoomArea.height);


            GUI.DrawTextureWithTexCoords(texPosition, backgroundTex, texCoords,false);
        }
        private void DrawConnections(DialogueNode node)
        {
            Vector2 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            startPosition = ConvertScreenCoordsToZoomCoords(startPosition) * currentZoom - zoomCoordsOrigin * (1 + currentZoom);
            foreach (DialogueNode childNode in selectedDialogue.GetNodeChildren(node))
            {
                Vector2 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                endPosition = ConvertScreenCoordsToZoomCoords(endPosition) * currentZoom - zoomCoordsOrigin * (1 + currentZoom);

                Vector2 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.green, null, 4f);
            }
        }

        #region Node Drawers
        private void DrawNode(DialogueNode node)
        {
            GUIStyle nodeStyle = node.IsPlayerSpeaking() ? playerNodeStyle : npcNodeStyle;
            var nodeRect = node.GetRect();
            nodeRect.position -= zoomCoordsOrigin;
            GUILayout.BeginArea(nodeRect, nodeStyle);
            DrawNameOverride(node);
            DrawNodeText(node);
            DrawShortDescription(node);
            GUILayout.Space(5);
            DrawNodeButtons(node);
            DrawEnterAction(node);
            DrawExitAction(node);
            DrawConditionManipulators(node);
            DrawNodeType(node);

            GUILayout.EndArea();
        }

        private void DrawNameOverride(DialogueNode node)
        {
            if (node.IsPlayerSpeaking()) return;
            EditorGUI.BeginChangeCheck();
            GUILayout.Label("Name Override", new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontSize = DEFAULT_FONT_SIZE });
            string newName = GUILayout.TextField(node.GetNameOverride(), textFieldStyle);

            if (EditorGUI.EndChangeCheck())
            {
                node.SetNameOverride(newName);
            }

        }
        private void DrawNodeText(DialogueNode node)
        {
            EditorGUILayout.LabelField("Node Text:", textStyle);
            EditorGUI.BeginChangeCheck();
            SetTextAreaStyle();
            string newText = EditorGUILayout.TextArea(node.GetText(), textAreaStyle, GUILayout.MinHeight(node.GetRect().width / 7));
            if (EditorGUI.EndChangeCheck())
            {
                node.SetText(newText);
            }
        }
        private void DrawShortDescription(DialogueNode node)
        {
            if (!node.IsPlayerSpeaking()) return;
            GUILayout.Label("Short description:", textStyle);
            EditorGUI.BeginChangeCheck();
            string newDesc = GUILayout.TextField(node.GetShortDescription(), textFieldStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetShortDescription(newDesc);
            }
        }

        private void DrawNodeButtons(DialogueNode node)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("-", buttonStyle))
            {
                removingNode = node;
            }
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link", buttonStyle))
                {
                    linkingParentNode = node;
                }
            }
            else if (node != linkingParentNode)
            {
                if (!linkingParentNode.GetChildren().Contains(node.name)) // case when we don't have a such child
                {
                    if (GUILayout.Button("child", buttonStyle))
                    {
                        linkingParentNode.AddChild(node.name);
                        linkingParentNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("unchild", buttonStyle))
                    {
                        linkingParentNode.RemoveChild(node.name);
                        linkingParentNode = null;
                    }
                }
            }
            else
            {
                if (GUILayout.Button("cancel", buttonStyle))
                {
                    linkingParentNode = null;
                }
            }
            if (GUILayout.Button("+", buttonStyle))
            {
                creatingNode = node;
            }
            GUILayout.EndHorizontal();
        }

        private  void DrawEnterAction(DialogueNode node)
        {
            EditorGUILayout.LabelField("Node Enter Action:", textStyle);
            EditorGUI.BeginChangeCheck();

            string newEnterAction = GUILayout.TextField(node.GetOnEnterAction(), textFieldStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetOnEnterAction(newEnterAction);
            }
        }

        private  void DrawExitAction(DialogueNode node)
        {
            EditorGUILayout.LabelField("Node Exit Action:", textStyle);

            EditorGUI.BeginChangeCheck();
            string newExitAction = GUILayout.TextField(node.GetOnExitAction(),textFieldStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetOnExitAction(newExitAction);
            }
        }
        private void DrawConditionManipulators(DialogueNode node)
        {
            if (conditionCopyNode == null)
            {
                if (GUILayout.Button("Copy Condition", buttonStyle))
                {
                    conditionCopyNode = node;
                }
            }
            else
            {
                if (conditionCopyNode == node)
                {
                    if (GUILayout.Button("Cancel Condition Copy", buttonStyle))
                    {
                        conditionCopyNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("Paste Condition", buttonStyle))
                    {
                        node.SetCondition(conditionCopyNode.GetCondition());
                        conditionCopyNode = null;
                    }
                }
            }
            if (GUILayout.Button("Add New AND"))
            {
                node.GetCondition().GetDisjunctions().Add(new Condition.Disjunction());
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Negate Condition", textStyle);
            EditorGUI.BeginChangeCheck();

            var newNegation = EditorGUILayout.Toggle(node.GetCondition().GetNegation());

            if (EditorGUI.EndChangeCheck())
            {
                node.GetCondition().SetNegation(newNegation);
            }
            GUILayout.EndHorizontal();
        }
        private void DrawNodeType(DialogueNode node)
        {
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            string nodeType = node.IsPlayerSpeaking() ? "Player" : "NPC";

            Color typeColor = node == Selection.activeObject ? Color.blue : Color.black;

            GUIStyle nodeTypeStyle = new GUIStyle()  {alignment = TextAnchor.LowerRight, fontSize = 20 };
            nodeTypeStyle.normal.textColor = typeColor;

            bool newIsPlayerSpeaking = GUILayout.Toggle(node.IsPlayerSpeaking(),
                new GUIContent() { text = nodeType + " Node" },
                    nodeTypeStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetPlayerSpeaking(newIsPlayerSpeaking);
            }
            Repaint();
        }

        #region Node Condition Drawers
        private void DrawCondition(DialogueNode node)
        {
            Vector2 startPredicatePosition = node.GetRect().center - new Vector2(0, Y_CONDITION_OFFSET) - zoomCoordsOrigin;
            var disjunctions = node.GetCondition().GetDisjunctions();
            if (disjunctions == null) return;
            int totalPredicatesCount = disjunctions.Sum((x) =>
            {
                int c;
                c = x.GetConjunctions().Count;
                if (c == 0) c = 1;
                return c;
            }); //If we have 0 - then area can't be properly assigned 

            Rect conditionRect = GetMultiObjectArea(startPredicatePosition, totalPredicatesCount, CONDITION_SIZE, MAX_CONDITIONAREA_HEIGHT);
            DrawDisjunctions(node, disjunctions, conditionRect);
        }
        private void DrawDisjunctions(DialogueNode node, List<Condition.Disjunction> disjunctions, Rect multiNodeRect)
        {
            GUILayout.BeginArea(multiNodeRect);
            GUILayout.BeginHorizontal();
            var conjunctionStyle = node.GetCondition().GetNegation() ? negatedPredicateNodeStyle : defaultDisjunctionNodeStyle;
            foreach (var disjunction in disjunctions)
            {
                GUILayout.BeginVertical(conjunctionStyle);
                DrawDisjunction(disjunction);
                GUILayout.EndVertical();
            }
            if (disjToRemove != null)
            {
                disjunctions.Remove(disjToRemove);
                disjToRemove = null;
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        private void DrawDisjunction(Condition.Disjunction disjunction)
        {
            GUILayout.Label("AND", new GUIStyle(textStyle) { alignment = TextAnchor.UpperCenter, fontSize = 20 });

            if (GUILayout.Button("Remove"))
            {
                disjToRemove = disjunction;
            }
            if (GUILayout.Button("Add New OR"))
            {
                disjunction.GetConjunctions().Add(new Condition.Predicate());
            }
            var conjunctions = disjunction.GetConjunctions();
            GUILayout.BeginHorizontal();
            foreach (var conjunction in conjunctions)
            {
                DrawConjunction(conjunction);
            }
            GUILayout.EndHorizontal();
            if (conjToRemove != null)
            {
                conjunctions.Remove(conjToRemove);
                conjToRemove = null;
            }

        }

        private void DrawConjunction(Condition.Predicate conjunction)
        {
            var conjStyle = conjunction.GetNegation() ? negatedPredicateNodeStyle : conjunctionNodeStyle;
            GUILayout.BeginVertical(conjStyle);
            GUILayout.Label("OR", labelStyle);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Negate", textStyle);
            EditorGUI.BeginChangeCheck();
            bool newNegation = EditorGUILayout.Toggle(conjunction.GetNegation());
            if (EditorGUI.EndChangeCheck())
            {
                conjunction.SetNegation(newNegation);
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Remove"))
            {
                conjToRemove = conjunction;
            }
            EditorGUILayout.BeginHorizontal();
            var currentPredicate = conjunction.GetPredicate();
            var predicateEnumLength = Enum.GetValues(typeof(PredicateEnum)).Length - 1;


            if (GUILayout.Button("<"))
            {
                if (currentPredicate == 0)
                {
                    conjunction.SetPredicate((PredicateEnum)predicateEnumLength);
                }
                else
                {
                    conjunction.SetPredicate(--currentPredicate);
                }
            }
            EditorGUI.BeginChangeCheck();
            var popupStyle = new GUIStyle(EditorStyles.popup) { fontSize = 12, alignment = TextAnchor.MiddleCenter };
            popupStyle.normal.textColor = Color.black;
            var newPredicate = EditorGUILayout.EnumPopup(currentPredicate, popupStyle);
            if (EditorGUI.EndChangeCheck())
            {
                conjunction.SetPredicate((PredicateEnum)newPredicate);
            }
            if (GUILayout.Button(">"))
            {
                if ((int)currentPredicate == predicateEnumLength)
                {
                    conjunction.SetPredicate(0);
                }
                else
                {
                    conjunction.SetPredicate(++currentPredicate);
                }
            }
            EditorGUILayout.EndHorizontal();
            DrawPredicateParametrs(conjunction);

            GUILayout.EndVertical();
        }

        private void DrawPredicateParametrs(Condition.Predicate conjunction)
        {
            string paramToRemove = null;
            string paramToChange = null;
            string newParam = "";
            foreach (string param in conjunction.GetParametrs())
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();
                newParam = GUILayout.TextField(param, textFieldStyle);
                if (EditorGUI.EndChangeCheck())
                {
                    paramToChange = param;
                }
                if (GUILayout.Button("-", buttonStyle))
                {
                    paramToRemove = param;
                }
                EditorGUILayout.EndHorizontal();
            }
            if (paramToChange != null)
            {
                conjunction.SetParametr(paramToChange, newParam);
            }
            if (paramToRemove != null)
            {
                conjunction.RemoveParametr(paramToRemove);
            }
            if (GUILayout.Button("Add Parametr", new GUIStyle(GUI.skin.button) { fontSize = DEFAULT_FONT_SIZE }))
            {
                conjunction.AddNewParametr();
            }
        }

        #endregion

        #endregion
        #endregion
        #region Interaction Events
        private void HandleEvents()
        {
            if (Event.current.control && (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.D))
            {
                HandleNodeCreate();
            }
            if (Event.current.type == EventType.KeyDown && (Event.current.control && 
                ( Event.current.keyCode == KeyCode.X) || Event.current.keyCode == KeyCode.Delete ))
            {
                HandleNodeRemove();
            }
            if (Event.current.isScrollWheel)
            {
                HandleZoom();
                Event.current.Use();
            }

            if (Event.current.type == EventType.MouseDown)
            {
                HandleMouseDown();
            }
            else if (Event.current.type == EventType.MouseDrag)
            {
                HandleDragging();
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                draggingNode = null;
                draggingCanvas = false;
            }

        }
        private void HandleNodeCreate()
        {
            var selectedNode = Selection.activeObject as DialogueNode;
            if (selectedNode != null)
            {
                selectedDialogue.CreateNode(selectedNode);
            }
            Repaint();
        }
        private void HandleNodeRemove()
        {
            var selectedNode = Selection.activeObject as DialogueNode;
            if (selectedNode != null)
            {
                selectedDialogue.RemoveNode(selectedNode);
            }
            Repaint();
        }

      
        private void TryCreateNode()
        {
            if (creatingNode != null)
            {
                selectedDialogue.CreateNode(creatingNode);
                creatingNode = null;
            }
        }
        private void TryRemoveNode()
        {
            if (removingNode != null)
            {
                selectedDialogue.RemoveNode(removingNode);
                removingNode = null;
            }
        }
        private void HandleDragging()
        {
            if (draggingCanvas)
            {
                HandleCanvasDrag();
            }
            else if (draggingNode != null)
            {
                HandleNodeDrag();
            }
        }

        private void HandleMouseDown()
        {
            draggingNode = GetNodeAtPoint(ConvertScreenCoordsToZoomCoords(Event.current.mousePosition));

            if (draggingNode != null)
            {
                Selection.activeObject = draggingNode;
            }
            else
            {
                Selection.activeObject = selectedDialogue;
                draggingCanvas = true; //true when no node selected
            }
        }

        private void HandleCanvasDrag()
        {
            Vector2 delta = Event.current.delta;
            var zoomedDelta = delta / currentZoom;

            zoomCoordsOrigin -= zoomedDelta;
            AddTextureOffset(zoomedDelta);
            GUI.changed = true;
        }

        private void HandleNodeDrag()
        {
            Vector2 newNodePosition = draggingNode.GetRect().position + Event.current.delta / currentZoom;
            draggingNode.SetPosition(newNodePosition);
            GUI.changed = true;
        }

        private void HandleZoom()
        {
            Vector2 delta = Event.current.delta;
            Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(Event.current.mousePosition);
            float zoomDelta = -delta.y * ZOOM_SPEED;
            float oldZoom = currentZoom;
            currentZoom += zoomDelta;
            currentZoom = Mathf.Clamp(currentZoom, MIN_ZOOM, MAX_ZOOM);
            var previousZoomCoordsOrigin = zoomCoordsOrigin;
            zoomCoordsOrigin += (zoomCoordsMousePos - zoomCoordsOrigin) - (oldZoom / currentZoom) * (zoomCoordsMousePos - zoomCoordsOrigin);
            var coordsDelta = previousZoomCoordsOrigin - zoomCoordsOrigin;

            AddTextureOffset(coordsDelta);
        }
        #endregion
        #region Utility Methods
        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - zoomArea.TopLeft()) / currentZoom + zoomCoordsOrigin;
        }
        private void AddTextureOffset(Vector2 offsetToAdd)
        {
            offsetToAdd = new Vector2(offsetToAdd.x % BACKGROUND_SIZE, offsetToAdd.y % BACKGROUND_SIZE);
            textureOffset += offsetToAdd;
            
            float xRestrictor = textureOffset.x < -BACKGROUND_SIZE ? BACKGROUND_SIZE : textureOffset.x > BACKGROUND_SIZE ? -BACKGROUND_SIZE : 0;
            float yRestrictor = textureOffset.y < -BACKGROUND_SIZE ? BACKGROUND_SIZE : textureOffset.y > BACKGROUND_SIZE ? -BACKGROUND_SIZE : 0;
            textureOffset += new Vector2(xRestrictor, yRestrictor);
        }
        
        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            return selectedDialogue.GetAllNodes().Where((node) => node.GetRect().Contains(point)).LastOrDefault();
        }
        /// <summary>
        /// Gets area for multiple objects depending on total object count and object size
        /// </summary>
        /// <param name="startDrawPosition"></param>
        /// <param name="objectsCount"></param>
        /// <param name="objectSize"></param>
        /// <param name="maxAreaHeight"></param>
        /// <returns></returns>
        private Rect GetMultiObjectArea(Vector2 startDrawPosition, int objectsCount, float objectSize, float maxAreaHeight)
        {
            return new Rect(startDrawPosition - new Vector2(objectSize / 2 * objectsCount, 0),
                new Vector2(objectSize * objectsCount, maxAreaHeight));
        }

        #endregion
        #region Styling

        private GUIStyle GetConditionNodeStyle(string nodeName)
        {
            var condNodeStyle = GetNodeStyle(nodeName);
            condNodeStyle.fontSize = 15;
            condNodeStyle.alignment = TextAnchor.UpperCenter;
            return condNodeStyle;
        }
        private void SetTextFieldStyle()
        {
            textFieldStyle = new GUIStyle(EditorStyles.textField);
            textFieldStyle.fontSize = 18;
        }
        private void SetButtonStyle()
        {
            buttonStyle = GUI.skin.button;
            buttonStyle.fontSize = DEFAULT_FONT_SIZE;
            buttonStyle.normal.textColor = Color.black;
        }
        private void SetTextAreaStyle()
        {
            if (textAreaStyle != null) return;
            textAreaStyle = new GUIStyle(EditorStyles.textArea);
            textAreaStyle.fontSize = 15;
            textAreaStyle.wordWrap = true;
        }

        private GUIStyle GetNodeStyle(string nodeBGName)
        {
            GUIStyle style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load($"{nodeBGName}") as Texture2D;
            style.normal.textColor = Color.black;
            style.padding = new RectOffset(20, 20, 20, 20);
            style.border = new RectOffset(12, 12, 12, 12);
            return style;

        }
        #endregion

    }

}

