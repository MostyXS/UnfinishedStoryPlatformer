using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Dialogues
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Sprite playerImage;
        [SerializeField] string playerName;

        private Dialogue _currentDialogue;
        private DialogueNode _currentNode;
        private AIConversant _currentConversant;
        private bool _isChoosing;

        public event Action OnConversationUpdated;


        public void StartDialgoue(AIConversant newConversant, Dialogue newDialogue)
        {
            _currentConversant = newConversant;
            _currentDialogue = newDialogue;
            _currentNode = _currentDialogue.GetRootNode();
            _isChoosing = false;
            TriggerEnterAction();
            OnConversationUpdated?.Invoke();
        }


        public void Quit()
        {
            _currentDialogue = null;
            TriggerExitAction();
            _currentNode = null;
            _currentConversant = null;
            _isChoosing = false;
            OnConversationUpdated?.Invoke();
        }

        public bool IsActive()
        {
            return _currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return _isChoosing;
        }

        public string GetText()
        {
            if (_currentDialogue == null) return "";

            return _currentNode.GetText();
        }

        //TO CORRECT
        public string GetCurrentConversantName()
        {
            if (_currentNode.GetNameOverride() != "")
            {
                return _currentNode.GetNameOverride();
            }
            else
            {
                if (_isChoosing)
                {
                    return playerName;
                }
                else
                {
                    return _currentConversant.GetName();
                }
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            _currentNode = chosenNode;
            TriggerEnterAction();
            _isChoosing = false;
            Next();
        }

        public void Next()
        {
            var playerResponses = FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode));
            if (playerResponses.Any())
            {
                _isChoosing = true;
                TriggerExitAction();
                OnConversationUpdated?.Invoke();
                return;
            }


            DialogueNode[] children = _currentDialogue.GetAiChildren(_currentNode).ToArray();
            var randomIndex = UnityEngine.Random.Range(0, children.Length);
            TriggerExitAction();
            _currentNode = children[randomIndex];
            OnConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return _currentDialogue.GetNodeChildren(_currentNode).Count() != 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (DialogueNode node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if (_currentNode != null)
            {
                TriggerAction(_currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (_currentNode != null)
            {
                TriggerAction(_currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;
            _currentConversant.TriggerAllActions(action);
        }
    }
}