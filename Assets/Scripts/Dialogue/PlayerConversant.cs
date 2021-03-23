using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Predication;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private PlayerInput _playerInput;
        public event Action OnConversationUpdated;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        public void StartDialgoue(AIConversant newConversant, Dialogue newDialogue)
        {
            _currentConversant = newConversant;
            _currentDialogue = newDialogue;
            _currentNode = _currentDialogue.GetRootNode();
            _isChoosing = false;
            TriggerEnterAction();
            OnConversationUpdated?.Invoke();
            _playerInput.SwitchCurrentActionMap("UI");
        }


        public void Quit()
        {
            _currentDialogue = null;
            TriggerExitAction();
            _currentNode = null;
            _currentConversant = null;
            _isChoosing = false;
            OnConversationUpdated?.Invoke();
            _playerInput.SwitchCurrentActionMap("Player");
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
            if (string.IsNullOrEmpty(_currentNode.GetNameOverride())) return _currentNode.GetNameOverride();

            if (_isChoosing || _currentNode.IsPlayerSpeaking())
                return playerName;
            else
                return _currentConversant.GetName();
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
            OnConversationUpdated?.Invoke();
        }

        public void Next()
        {
            var playerResponses = FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode)).ToArray();
            if (playerResponses.Length == 1)
            {
                TriggerExitAction();
                _currentNode = playerResponses[0];
                OnConversationUpdated?.Invoke();
                return;
            }

            if (playerResponses.Length > 1)
            {
                _isChoosing = true;
                TriggerExitAction();
                OnConversationUpdated?.Invoke();
                return;
            }


            DialogueNode[] children = _currentDialogue.GetAIChildren(_currentNode).ToArray();
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

        public Sprite GetImage()
        {
            if (_isChoosing || _currentNode.IsPlayerSpeaking())
                return playerImage;
            else
                return _currentConversant.GetImage();
        }
    }
}