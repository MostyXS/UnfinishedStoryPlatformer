using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.Collectioning.Editor
{
    public class AtlasEditor : EditorWindow
    {
        private static Atlas _selectedAtlas;
        private AtlasCategory _selectedCategory;
        private AtlasObject _selectedAtlasObject;

        private const float ObjectImageSize = 128f;
        private Vector2 _scrollPosition = Vector2.zero;
        private const float Spacing = 10f;


        #region Unity Callbacks

        [UsedImplicitly]
        [MenuItem("Game/Atlas Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<AtlasEditor>();
            window.titleContent = new GUIContent("AtlasCategory");
            window.Show();
        }

        private void OnGUI()
        {
            if (!_selectedAtlas)
            {
                GUILayout.Label("No atlas selected");
                return;
            }

            DrawAtlas();
        }

        [UsedImplicitly]
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            var newAtlas = EditorUtility.InstanceIDToObject(instanceId) as Atlas;
            if (newAtlas == null) return false;


            ShowWindow();
            _selectedAtlas = newAtlas;

            return true;
        }

        private void OnSelectionChange()
        {
            var newAtlas = Selection.activeObject as Atlas;

            if (newAtlas == null || _selectedAtlas == newAtlas) return;

            _selectedAtlas = newAtlas;
        }

        private void DrawAtlas()
        {
            GUILayout.BeginHorizontal();
            DrawCategorySelector();
            if (_selectedCategory != null)
            {
                DrawSelectedCategory();
            }

            if (_selectedAtlasObject != null)
            {
                DrawObject();
            }
            else
            {
                EditorGUILayout.LabelField("No Object Selected");
            }

            GUILayout.EndHorizontal();
        }

        private void DrawObject()
        {
            if (_selectedAtlasObject != null)
            {
                DrawObjectUi();
            }
            else
            {
                GUILayout.Label("No atlas selected");
            }
        }

        #endregion

        #region Object Drawers

        private void DrawObjectUi()
        {
            DrawTitle();
            DrawImage();
            DrawDescription();
        }

        private void DrawDescription()
        {
            var textArea = new Rect(position.width / 3 + Spacing, ObjectImageSize + Spacing,
                position.width * 2 / 3 - Spacing,
                position.height - ObjectImageSize - Spacing);
            var textAreaStyle = new GUIStyle(EditorStyles.textArea) {fontSize = 30};
            EditorGUI.BeginChangeCheck();
            var newDesc = GUI.TextArea(textArea, _selectedAtlasObject.GetDescription(), textAreaStyle);
            if (EditorGUI.EndChangeCheck())
            {
                _selectedAtlasObject.SetDescription(newDesc);
            }
        }

        private void DrawTitle()
        {
            var evt = Event.current;
            var textArea = new Rect(position.width / 3 + Spacing, 0,
                position.width - position.width / 3 - ObjectImageSize - Spacing    ,
                ObjectImageSize);
            var textAreaStyle = new GUIStyle(EditorStyles.textArea)
            {
                fontSize = 75, alignment = TextAnchor.MiddleCenter
            };
            if (textArea.Contains(evt.mousePosition))
            {
                EditorGUI.BeginChangeCheck();
                var newText = GUI.TextArea(textArea, _selectedAtlasObject.GetTitle(), textAreaStyle);
                if (EditorGUI.EndChangeCheck())
                {
                    _selectedAtlasObject.SetTitle(newText);
                }
            }
            else
            {
                GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
                {
                    fontSize = 75, alignment = TextAnchor.MiddleCenter
                };

                GUI.Label(textArea, _selectedAtlasObject.GetTitle(), labelStyle);
            }
        }

        private void DrawImage()
        {
            int currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 228;
            var evt = Event.current;
            var dropArea = new Rect(position.width - ObjectImageSize, 0, ObjectImageSize, ObjectImageSize);
            if (_selectedAtlasObject.GetImage() != null)
            {
                GUI.Box(dropArea, _selectedAtlasObject.GetImage().texture);
            }
            else
            {
                GUI.Box(dropArea, "Drop your image here");
            }

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                {
                    if (!dropArea.Contains(evt.mousePosition)) break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        var draggedImage = DragAndDrop.objectReferences[0] as Sprite;
                        if (!draggedImage) return;


                        _selectedAtlasObject.SetImage(draggedImage);
                    }
                }
                    break;
                case EventType.MouseDown:
                {
                    if (!dropArea.Contains(evt.mousePosition)) break;
                    EditorGUIUtility.ShowObjectPicker<Sprite>(_selectedAtlasObject.GetImage(), false, "",
                        currentPickerWindow);
                }

                    break;
            }

            if (Event.current.commandName == "ObjectSelectorUpdated")
            {
                var image = EditorGUIUtility.GetObjectPickerObject() as Sprite;
                _selectedAtlasObject.SetImage(image);
                Repaint();
            }
        }

        #endregion

        #region Toolbar Drawers

        private void DrawSelectedCategory()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandHeight(false));

            int index = 0;
            AtlasObject objectToRemove = null;
            foreach (var aObj in _selectedCategory.GetAllObjects())
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("-", new GUIStyle(GUI.skin.button) {alignment = TextAnchor.MiddleCenter},
                    GUILayout.ExpandWidth(false)))
                {
                    objectToRemove = aObj;
                }

                var objectButtonStyle = new GUIStyle(GUI.skin.button);
                if (_selectedAtlasObject == aObj)
                {
                    objectButtonStyle.fontStyle = FontStyle.Bold;
                    objectButtonStyle.fontSize += 5;
                }

                if (GUILayout.Button($"{aObj.GetTitle()} {index}", objectButtonStyle))
                {
                    _selectedAtlasObject = aObj;
                    Selection.activeObject = aObj;
                }

                GUILayout.EndHorizontal();
                index++;
            }

            if (objectToRemove != null)
            {
                _selectedCategory.RemoveObject(objectToRemove);
            }

            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Create new object"))
            {
                var nObj = _selectedCategory.AddObject();
                nObj.SetTitle("Title");
                nObj.SetDescription("Description");
                Selection.activeObject = nObj;
                _selectedAtlasObject = nObj;
            }

            if (_selectedAtlasObject != null &&  GUILayout.Button("Add Opener To Selected Object"))
            {
                
            }
            GUILayout.EndVertical();
        }

        private void DrawCategorySelector()
        {
            var toolbarWidth = position.width / 3;

            GUILayout.BeginVertical(GUILayout.Width(toolbarWidth));
            EditorGUI.BeginChangeCheck();
            if (_selectedCategory == null)
            {
                _selectedCategory = _selectedAtlas.GetCategoryByType(AtlasCategoryType.Character);
            }

            var newType = (AtlasCategoryType) EditorGUILayout.EnumPopup(_selectedCategory.GetCategoryType()
                , new GUIStyle(EditorStyles.popup) {alignment = TextAnchor.MiddleCenter},
                GUILayout.Width(toolbarWidth));
            if (EditorGUI.EndChangeCheck())
            {
                _selectedCategory = _selectedAtlas.GetCategoryByType(newType);
                _selectedAtlasObject = null;
            }
        }

        #endregion
    }
}