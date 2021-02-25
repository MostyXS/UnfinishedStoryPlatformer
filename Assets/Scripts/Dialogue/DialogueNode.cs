using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Dialogues
{
    [Serializable]
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        private string nameOverride = "";
        [SerializeField]
        private bool isPlayerSpeaking = false; //Can be enum
        [SerializeField]
        private string shortDescription;
        [SerializeField]
        [TextArea(3,50)]
        private string text;
        [SerializeField]
        private List<string> children = new List<string>();
        [SerializeField]
        private string onEnterAction;
        [SerializeField]
        private string onExitAction;
        [SerializeField]
        private Condition condition;

        [SerializeField]
        private Rect rect = new Rect(0, 0, 300, 400);



        public string GetText()
        {
            return text;
        }
        public IEnumerable<string> GetChildren()
        {
            return children;
        }
        public Rect GetRect()
        {
            return rect;
        }
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
        public string GetOnEnterAction()
        {
            return onEnterAction;
        }
        
        public string GetOnExitAction()
        {
            return onExitAction;
        }
        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }
        public string GetNameOverride()
        {
            return nameOverride;
        }
#if UNITY_EDITOR
        #region Condition Editor
        public Condition GetCondition()
        {
            Undo.RecordObject(this, "Condition Update");
            EditorUtility.SetDirty(this);
            return condition;
        }
        public void SetCondition(Condition newCondition)
        {
            Undo.RecordObject(this, "Node Condition Update");
            condition = new Condition(newCondition);
            EditorUtility.SetDirty(this);

        }
        
        #endregion
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Update Node Position");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Node Text");
                text = newText;
                EditorUtility.SetDirty(this);

            }
        }
        public void SetOnEnterAction(string newEnterAction)
        {
            if (newEnterAction != onEnterAction)
            {
                Undo.RecordObject(this, "Update Node Enter Action");
                onEnterAction = newEnterAction;
                EditorUtility.SetDirty(this);

            }
        }
        public void SetOnExitAction(string newExitAction)
        {
            if (newExitAction != onExitAction)
            {
                Undo.RecordObject(this, "Update Node Exit Action");
                onExitAction = newExitAction;
                EditorUtility.SetDirty(this);

            }
        }
        public void AddChild(string childId)
        {
            Undo.RecordObject(this, "Add Node Child");
            children.Add(childId);
            EditorUtility.SetDirty(this);

        }

        public void RemoveChild(string childId)
        {
            Undo.RecordObject(this, "Remove Node Child");
            children.Remove(childId);
            EditorUtility.SetDirty(this);

        }

        public void SetPlayerSpeaking(bool value)
        {
            Undo.RecordObject(this, "Update Node Player Speaking");
            isPlayerSpeaking = value;
            EditorUtility.SetDirty(this);
        }

        public void SetNameOverride(string newName)
        {
            Undo.RecordObject(this, "Update Node Name Override");
            nameOverride = newName;
            EditorUtility.SetDirty(this);
        }

        public void SetShortDescription(string newShortDescription)
        {
            Undo.RecordObject(this, "Update Node Short Description");
            shortDescription = newShortDescription;
            EditorUtility.SetDirty(this);
        }

        public string GetShortDescription()
        {
            return shortDescription;
        }

        
        


#endif
    }

}