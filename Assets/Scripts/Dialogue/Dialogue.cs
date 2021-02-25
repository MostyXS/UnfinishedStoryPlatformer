using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Dialogues
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Game/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {

        [SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();
#if UNITY_EDITOR
        [SerializeField] private Vector2 newNodeOffset = new Vector2(450, 0);
        private Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node; //Fills Node Lookup
            }
        }
#endif
        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach(DialogueNode node in GetNodeChildren(currentNode))
            {
                if(node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }
        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetNodeChildren(currentNode))
            {
                if (!node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }
        
        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }
        public IEnumerable<DialogueNode> GetNodeChildren(DialogueNode parentNode)
        {
            foreach(string nodeId in parentNode.GetChildren())
            {
                DialogueNode node;
                if (nodeLookup.TryGetValue(nodeId, out node))
                {
                    yield return node;
                }
            }
        }
#if UNITY_EDITOR

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Create Dialogue Node");
            Undo.RecordObject(this, "Add Dialogue Node");
            AddNode(newNode);
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            var newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }
            return newNode;
        }

        public void RemoveNode(DialogueNode nodeToRemove)
        {
            Undo.RecordObject(this, "Remove Dialogue Node");

            nodes.Remove(nodeToRemove);
            CleanDanglingChildren(nodeToRemove);
            OnValidate();
            Undo.DestroyObjectImmediate(nodeToRemove);
        }

        private void CleanDanglingChildren(DialogueNode nodeToRemove)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToRemove.name);
            }
        }
#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                AddNode(MakeNode(null));
            }
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this)))  
            {
                foreach(DialogueNode node in GetAllNodes())
                {
                    if(string.IsNullOrEmpty(AssetDatabase.GetAssetPath(node)))
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }

                }
            }
#endif
        }
        public void OnAfterDeserialize()
        {

        }

        
    }
}