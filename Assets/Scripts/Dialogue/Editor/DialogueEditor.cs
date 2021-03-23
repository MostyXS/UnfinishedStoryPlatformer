using Game.Utils.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Predication;
using Game.Utils.Editor.Extensions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.Dialogues.Editor
{
    public class DialogueEditor : EditorWindow
    {
        [NonSerialized] private static Dialogue _selectedDialogue;

        #region Styles Data

        [NonSerialized] private GUIStyle _labelStyle;
        [NonSerialized] private GUIStyle _conjunctionNodeStyle;
        [NonSerialized] private GUIStyle _defaultDisjunctionNodeStyle;
        [NonSerialized] private GUIStyle _negatedPredicateNodeStyle;
        [NonSerialized] private GUIStyle _npcNodeStyle;
        [NonSerialized] private GUIStyle _playerNodeStyle;
        [NonSerialized] private GUIStyle _textAreaStyle;
        [NonSerialized] private GUIStyle _textFieldStyle;
        [NonSerialized] private GUIStyle _textStyle;
        [NonSerialized] private GUIStyle _buttonStyle;

        #endregion

        #region Interaction Data

        [NonSerialized] private DialogueNode _draggingNode;
        [NonSerialized] private DialogueNode _creatingNode;
        [NonSerialized] private DialogueNode _removingNode;
        [NonSerialized] private DialogueNode _linkingParentNode;
        [NonSerialized] private DialogueNode _conditionCopyNode;
        [NonSerialized] private bool _draggingCanvas;
        [NonSerialized] private Condition.Disjunction _disjunctionToRemove;
        [NonSerialized] private Condition.Predicate _conjToRemove;

        #endregion

        #region Const Data

        [NonSerialized] private const int DefaultFontSize = 15;
        [NonSerialized] private const float BackgroundSize = 120f;
        [NonSerialized] private const float ZoomSpeed = .01f;
        [NonSerialized] private const float MinZoom = .2f;
        [NonSerialized] private const float MaxZoom = 5f;
        [NonSerialized] private const float ConditionSize = 250f;
        [NonSerialized] private const float MaxConditionAreaHeight = 600f;
        [NonSerialized] private const float YConditionOffset = -300;

        #endregion

        #region Zoom data

        [NonSerialized] private Vector2 _zoomCoordsOrigin = Vector2.zero;
        [NonSerialized] private readonly Rect _zoomArea = new Rect(0f, 0f, 8000f, 6000f);
        [NonSerialized] private float _currentZoom = Mathf.Lerp(MinZoom, MaxZoom, .1f);
        [NonSerialized] private Vector2 _textureOffset = Vector2.zero;

        #endregion

        #region Unity Callbacks

        [MenuItem("Game/Dialogue Editor")]
        private static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor").Focus();
            
        }

        [OnOpenAsset(1)]
        [UsedImplicitly]
        private static bool OnOpenAsset(int instanceId, int line)
        {
            var newDialogue = EditorUtility.InstanceIDToObject(instanceId) as Dialogue;
            if (newDialogue != null)
            {
                if (newDialogue != _selectedDialogue)
                    _selectedDialogue = newDialogue;
                
                
                ShowEditorWindow();
                return true;
            }

            return false;
        }

        private void OnEnable()
        {
            if (_selectedDialogue != null)
                _zoomCoordsOrigin = _selectedDialogue.GetRootNode().GetRect().position
                                    - new Vector2(position.width / 2, position.height / 2);


            _textStyle = new GUIStyle() {fontSize = DefaultFontSize};
            _npcNodeStyle = GetNodeStyle("node2");
            _playerNodeStyle = GetNodeStyle("node3");

            _negatedPredicateNodeStyle = GetConditionNodeStyle("node6");
            _defaultDisjunctionNodeStyle = GetConditionNodeStyle("node4");
            _conjunctionNodeStyle = GetConditionNodeStyle("node1");
            _labelStyle = new GUIStyle {fontSize = 20, alignment = TextAnchor.UpperCenter};
            Selection.selectionChanged += OnSelectionChanged;
            OnSelectionChanged();
        }

        private void OnGUI()
        {
            SetButtonStyle();
            SetTextFieldStyle();

            if (_selectedDialogue == null)
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
            if (newDialogue != null && newDialogue != _selectedDialogue)
            {
                _selectedDialogue = newDialogue;
                _zoomCoordsOrigin = _selectedDialogue.GetRootNode().GetRect().position;
                Repaint();
            }
        }

        #endregion

        #region GUI Drawers

        private void ProcessEditorDraw()
        {
            HandleEvents();
            EditorZoomArea.Begin(_currentZoom, _zoomArea);

            DrawBackground();
            DrawNodesAndNodeConnections();
            TryRemoveNode();
            TryCreateNode();

            EditorZoomArea.End();
        }

        private void DrawNodesAndNodeConnections()
        {
            foreach (DialogueNode node in _selectedDialogue.GetAllNodes())
            {
                DrawConnections(node);
            }

            foreach (DialogueNode node in _selectedDialogue.GetAllNodes())
            {
                DrawNode(node);
                DrawCondition(node);
            }
        }

        private void DrawBackground()
        {
            var backgroundTex = Resources.Load("background") as Texture2D;
            Rect texCoords = new Rect(0, 0,
                _zoomArea.width / BackgroundSize,
                _zoomArea.height / BackgroundSize);
            Rect texPosition = new Rect(-BackgroundSize + _textureOffset.x,
                -BackgroundSize + _textureOffset.y,
                _zoomArea.width, _zoomArea.height);


            GUI.DrawTextureWithTexCoords(texPosition, backgroundTex, texCoords, false);
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector2 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            startPosition = ConvertScreenCoordsToZoomCoords(startPosition) * _currentZoom -
                            _zoomCoordsOrigin * (1 + _currentZoom);
            foreach (DialogueNode childNode in _selectedDialogue.GetNodeChildren(node))
            {
                Vector2 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                endPosition = ConvertScreenCoordsToZoomCoords(endPosition) * _currentZoom -
                              _zoomCoordsOrigin * (1 + _currentZoom);

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
            GUIStyle nodeStyle = node.IsPlayerSpeaking() ? _playerNodeStyle : _npcNodeStyle;
            var nodeRect = node.GetRect();
            nodeRect.position -= _zoomCoordsOrigin;
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
            GUILayout.Label("Name Override",
                new GUIStyle() {alignment = TextAnchor.MiddleCenter, fontSize = DefaultFontSize});
            string newName = GUILayout.TextField(node.GetNameOverride(), _textFieldStyle);

            if (EditorGUI.EndChangeCheck())
            {
                node.SetNameOverride(newName);
            }
        }

        private void DrawNodeText(DialogueNode node)
        {
            EditorGUILayout.LabelField("Node Text:", _textStyle);
            EditorGUI.BeginChangeCheck();
            SetTextAreaStyle();
            var currentStyle = new GUIStyle(_textAreaStyle)
                {fixedHeight = _textAreaStyle.CalcHeight(new GUIContent(node.GetText()), node.GetRect().width)};
            string newText = EditorGUILayout.TextArea(node.GetText(), currentStyle);


            if (EditorGUI.EndChangeCheck())
            {
                node.SetText(newText);
                Repaint();
            }
        }

        private void DrawShortDescription(DialogueNode node)
        {
            if (!node.IsPlayerSpeaking()) return;
            GUILayout.Label("Short description:", _textStyle);
            EditorGUI.BeginChangeCheck();
            string newDesc = GUILayout.TextField(node.GetShortResponse(), _textFieldStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetShortDescription(newDesc);
            }
        }

        private void DrawNodeButtons(DialogueNode node)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("-", _buttonStyle))
            {
                _removingNode = node;
            }

            if (_linkingParentNode == null)
            {
                if (GUILayout.Button("link", _buttonStyle))
                {
                    _linkingParentNode = node;
                }
            }
            else if (node != _linkingParentNode)
            {
                if (!_linkingParentNode.GetChildren().Contains(node.name)) // case when we don't have a such child
                {
                    if (GUILayout.Button("child", _buttonStyle))
                    {
                        _linkingParentNode.AddChild(node.name);
                        _linkingParentNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("unchild", _buttonStyle))
                    {
                        _linkingParentNode.RemoveChild(node.name);
                        _linkingParentNode = null;
                    }
                }
            }
            else
            {
                if (GUILayout.Button("cancel", _buttonStyle))
                {
                    _linkingParentNode = null;
                }
            }

            if (GUILayout.Button("+", _buttonStyle))
            {
                _creatingNode = node;
            }

            GUILayout.EndHorizontal();
        }

        private void DrawEnterAction(DialogueNode node)
        {
            EditorGUILayout.LabelField("Node Enter Action:", _textStyle);
            EditorGUI.BeginChangeCheck();

            string newEnterAction = GUILayout.TextField(node.GetOnEnterAction(), _textFieldStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetOnEnterAction(newEnterAction);
            }
        }

        private void DrawExitAction(DialogueNode node)
        {
            EditorGUILayout.LabelField("Node Exit Action:", _textStyle);

            EditorGUI.BeginChangeCheck();
            string newExitAction = GUILayout.TextField(node.GetOnExitAction(), _textFieldStyle);
            if (EditorGUI.EndChangeCheck())
            {
                node.SetOnExitAction(newExitAction);
            }
        }

        private void DrawConditionManipulators(DialogueNode node)
        {
            if (_conditionCopyNode == null)
            {
                if (GUILayout.Button("Copy Condition", _buttonStyle))
                {
                    _conditionCopyNode = node;
                }
            }
            else
            {
                if (_conditionCopyNode == node)
                {
                    if (GUILayout.Button("Cancel Condition Copy", _buttonStyle))
                    {
                        _conditionCopyNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("Paste Condition", _buttonStyle))
                    {
                        node.SetCondition(_conditionCopyNode.GetCondition());
                        _conditionCopyNode = null;
                    }
                }
            }

            if (GUILayout.Button("Add New AND", _buttonStyle))
            {
                node.GetCondition().GetDisjunctions().Add(new Condition.Disjunction());
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Negate Condition", _textStyle);
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

            GUIStyle nodeTypeStyle = new GUIStyle() {alignment = TextAnchor.LowerRight, fontSize = 20};
            nodeTypeStyle.normal.textColor = typeColor;

            bool newIsPlayerSpeaking = GUILayout.Toggle(node.IsPlayerSpeaking(),
                new GUIContent() {text = nodeType + " Node"},
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
            Vector2 startPredicatePosition =
                node.GetRect().center - new Vector2(0, YConditionOffset) - _zoomCoordsOrigin;
            var disjunctions = node.GetCondition().GetDisjunctions();
            if (disjunctions == null) return;
            int totalPredicatesCount = disjunctions.Sum((x) =>
            {
                int c;
                c = x.GetConjunctions().Count;
                if (c == 0) c = 1;
                return c;
            }); //If we have 0 - then area can't be properly assigned 

            Rect conditionRect = GetMultiObjectArea(startPredicatePosition, totalPredicatesCount, ConditionSize,
                MaxConditionAreaHeight);
            DrawDisjunctions(node, disjunctions, conditionRect);
        }

        private void DrawDisjunctions(DialogueNode node, List<Condition.Disjunction> disjunctions, Rect multiNodeRect)
        {
            GUILayout.BeginArea(multiNodeRect);
            GUILayout.BeginHorizontal();
            var conjunctionStyle = node.GetCondition().GetNegation()
                ? _negatedPredicateNodeStyle
                : _defaultDisjunctionNodeStyle;
            foreach (var disjunction in disjunctions)
            {
                GUILayout.BeginVertical(conjunctionStyle);
                DrawDisjunction(disjunction);
                GUILayout.EndVertical();
            }

            if (_disjunctionToRemove != null)
            {
                disjunctions.Remove(_disjunctionToRemove);
                _disjunctionToRemove = null;
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void DrawDisjunction(Condition.Disjunction disjunction)
        {
            GUILayout.Label("AND", new GUIStyle(_textStyle) {alignment = TextAnchor.UpperCenter, fontSize = 20});

            if (GUILayout.Button("Remove", _buttonStyle))
            {
                _disjunctionToRemove = disjunction;
            }

            if (GUILayout.Button("Add New OR", _buttonStyle))
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
            if (_conjToRemove != null)
            {
                conjunctions.Remove(_conjToRemove);
                _conjToRemove = null;
            }
        }

        private void DrawConjunction(Condition.Predicate conjunction)
        {
            var conjStyle = conjunction.GetNegation() ? _negatedPredicateNodeStyle : _conjunctionNodeStyle;
            GUILayout.BeginVertical(conjStyle);
            GUILayout.Label("OR", _labelStyle);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Negate", _textStyle);
            EditorGUI.BeginChangeCheck();
            bool newNegation = EditorGUILayout.Toggle(conjunction.GetNegation());
            if (EditorGUI.EndChangeCheck())
            {
                conjunction.SetNegation(newNegation);
            }

            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Remove", _buttonStyle))
            {
                _conjToRemove = conjunction;
            }

            EditorGUILayout.BeginHorizontal();
            var currentPredicate = conjunction.GetPredicate();
            var predicateEnumLength = Enum.GetValues(typeof(PredicateType)).Length - 1;


            if (GUILayout.Button("<"))
            {
                if (currentPredicate == 0)
                {
                    conjunction.SetPredicate((PredicateType) predicateEnumLength);
                }
                else
                {
                    conjunction.SetPredicate(--currentPredicate);
                }
            }

            var popupStyle = new GUIStyle(EditorStyles.popup) {fontSize = 17, alignment = TextAnchor.MiddleCenter};
            popupStyle.normal.textColor = Color.black;
            EditorGUI.BeginChangeCheck();
            var newPredicate = EditorGUILayout.EnumPopup(currentPredicate, popupStyle);
            if (EditorGUI.EndChangeCheck())
            {
                conjunction.SetPredicate((PredicateType) newPredicate);
            }

            if (GUILayout.Button(">"))
            {
                if ((int) currentPredicate == predicateEnumLength)
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
                newParam = GUILayout.TextField(param, _textFieldStyle);
                if (EditorGUI.EndChangeCheck())
                {
                    paramToChange = param;
                }

                if (GUILayout.Button("-", _buttonStyle, GUILayout.ExpandWidth(false)))
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

            if (GUILayout.Button("Add Parametr", _buttonStyle))
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
            if (Event.current.control &&
                (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.D))
            {
                HandleNodeCreate();
            }

            if (Event.current.type == EventType.KeyDown && (Event.current.control &&
                                                            (Event.current.keyCode == KeyCode.X) ||
                                                            Event.current.keyCode == KeyCode.Delete))
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
                _draggingNode = null;
                _draggingCanvas = false;
            }
        }

        private void HandleNodeCreate()
        {
            var selectedNode = Selection.activeObject as DialogueNode;
            if (selectedNode != null)
            {
                _selectedDialogue.CreateNode(selectedNode);
            }

            Repaint();
        }

        private void HandleNodeRemove()
        {
            var selectedNode = Selection.activeObject as DialogueNode;
            if (selectedNode != null)
            {
                _selectedDialogue.RemoveNode(selectedNode);
            }

            Repaint();
        }


        private void TryCreateNode()
        {
            if (_creatingNode != null)
            {
                _selectedDialogue.CreateNode(_creatingNode);
                _creatingNode = null;
            }
        }

        private void TryRemoveNode()
        {
            if (_removingNode != null)
            {
                _selectedDialogue.RemoveNode(_removingNode);
                _removingNode = null;
            }
        }

        private void HandleDragging()
        {
            if (_draggingCanvas)
            {
                HandleCanvasDrag();
            }
            else if (_draggingNode != null)
            {
                HandleNodeDrag();
            }
        }

        private void HandleMouseDown()
        {
            _draggingNode = GetNodeAtPoint(ConvertScreenCoordsToZoomCoords(Event.current.mousePosition));

            if (_draggingNode != null)
            {
                Selection.activeObject = _draggingNode;
            }
            else
            {
                Selection.activeObject = _selectedDialogue;
                _draggingCanvas = true; //true when no node selected
            }
        }

        private void HandleCanvasDrag()
        {
            Vector2 delta = Event.current.delta;
            var zoomedDelta = delta / _currentZoom;

            _zoomCoordsOrigin -= zoomedDelta;
            AddTextureOffset(zoomedDelta);
            GUI.changed = true;
        }

        private void HandleNodeDrag()
        {
            Vector2 newNodePosition = _draggingNode.GetRect().position + Event.current.delta / _currentZoom;
            _draggingNode.SetPosition(newNodePosition);
            GUI.changed = true;
        }

        private void HandleZoom()
        {
            Vector2 delta = Event.current.delta;
            Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(Event.current.mousePosition);
            float zoomDelta = -delta.y * ZoomSpeed;
            float oldZoom = _currentZoom;
            _currentZoom += zoomDelta;
            _currentZoom = Mathf.Clamp(_currentZoom, MinZoom, MaxZoom);
            var previousZoomCoordsOrigin = _zoomCoordsOrigin;
            _zoomCoordsOrigin += (zoomCoordsMousePos - _zoomCoordsOrigin) -
                                 (oldZoom / _currentZoom) * (zoomCoordsMousePos - _zoomCoordsOrigin);
            var coordsDelta = previousZoomCoordsOrigin - _zoomCoordsOrigin;

            AddTextureOffset(coordsDelta);
        }

        #endregion

        #region Utility Methods

        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - _zoomArea.TopLeft()) / _currentZoom + _zoomCoordsOrigin;
        }

        private void AddTextureOffset(Vector2 offsetToAdd)
        {
            offsetToAdd = new Vector2(offsetToAdd.x % BackgroundSize, offsetToAdd.y % BackgroundSize);
            _textureOffset += offsetToAdd;

            float xRestrictor = _textureOffset.x < -BackgroundSize ? BackgroundSize :
                _textureOffset.x > BackgroundSize ? -BackgroundSize : 0;
            float yRestrictor = _textureOffset.y < -BackgroundSize ? BackgroundSize :
                _textureOffset.y > BackgroundSize ? -BackgroundSize : 0;
            _textureOffset += new Vector2(xRestrictor, yRestrictor);
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            return _selectedDialogue.GetAllNodes().Where((node) => node.GetRect().Contains(point)).LastOrDefault();
        }

        /// <summary>
        /// Gets area for multiple objects depending on total object count and object size
        /// </summary>
        /// <param name="startDrawPosition"></param>
        /// <param name="objectsCount"></param>
        /// <param name="objectSize"></param>
        /// <param name="maxAreaHeight"></param>
        /// <returns></returns>
        private Rect GetMultiObjectArea(Vector2 startDrawPosition, int objectsCount, float objectSize,
            float maxAreaHeight)
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
            _textFieldStyle = new GUIStyle(EditorStyles.textField);
            _textFieldStyle.fontSize = 18;
        }

        private void SetButtonStyle()
        {
            _buttonStyle = new GUIStyle(GUI.skin.button);
            _buttonStyle.fontSize = DefaultFontSize;
            _buttonStyle.normal.textColor = Color.black;
            _buttonStyle.alignment = TextAnchor.MiddleCenter;
        }

        private void SetTextAreaStyle()
        {
            if (_textAreaStyle != null) return;
            _textAreaStyle = new GUIStyle(EditorStyles.textArea);
            _textAreaStyle.fontSize = 15;
            _textAreaStyle.wordWrap = true;
        }

        private GUIStyle GetNodeStyle(string nodeBgName)
        {
            GUIStyle style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load($"{nodeBgName}") as Texture2D;
            style.normal.textColor = Color.black;
            style.padding = new RectOffset(20, 20, 20, 20);
            style.border = new RectOffset(12, 12, 12, 12);
            return style;
        }

        #endregion
    }
}