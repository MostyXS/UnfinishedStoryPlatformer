using Game.Control;
using Game.Dialogues;
using Game.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("Character")] [SerializeField] private Sprite unknownImage;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI conversantName;

        [Header("Dialogue")] [SerializeField] private GameObject soloResponse;
        [SerializeField] private TextMeshProUGUI soloText;
        [SerializeField] private Transform choiceList;
        [SerializeField] private GameObject choicePrefab;

        [Header("Buttons")] [SerializeField] private Button nextButton;
        [SerializeField] private Button quitButton;


        private PlayerConversant _playerConversant;

        private void Start()
        {
            _playerConversant = Player.Instance.GetComponent<PlayerConversant>();
            _playerConversant.OnConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(Next);
            quitButton.onClick.AddListener(Quit);
            UpdateUI();
        }

        private void Quit()
        {
            _playerConversant.Quit();
        }

        private void Next()
        {
            _playerConversant.Next();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(_playerConversant.IsActive());
            if (!_playerConversant.IsActive()) return;

            var isChoosing = _playerConversant.IsChoosing();
            choiceList.gameObject.SetActive(isChoosing);

            conversantName.text = _playerConversant.GetCurrentConversantName();
            soloResponse.SetActive(!isChoosing);
            var conversantImage = _playerConversant.GetImage();
            image.sprite = conversantImage ? conversantImage : unknownImage;
            if (isChoosing)
            {
                BuildChoiceList();
            }
            else
            {
                soloText.text = _playerConversant.GetText();
                var hasNext = _playerConversant.HasNext();
                nextButton.gameObject.SetActive(hasNext);
                quitButton.gameObject.SetActive(!hasNext);
            }
        }

        private void BuildChoiceList()
        {
            choiceList.Clear();
            foreach (DialogueNode choice in _playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceList);
                var choiceText = !string.IsNullOrEmpty(choice.GetShortResponse())
                    ? choice.GetShortResponse()
                    : choice.GetText();
                choiceInstance.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
                Button button = choiceInstance.GetComponent<Button>();
                button.onClick.AddListener(() => { _playerConversant.SelectChoice(choice); });
            }
        }
    }
}