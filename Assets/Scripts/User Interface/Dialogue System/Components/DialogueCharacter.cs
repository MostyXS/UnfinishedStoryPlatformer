using Game.Interfaces;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues.Components
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Game/Dialogues/New Character")]
    public class DialogueCharacter : ScriptableObject, IInsertable
    {
        
        [SerializeField] string cName;
        [SerializeField] Sprite avatar;
        public void InsertIntoInterface(Transform i)
        {
            var textField = i.Find(UILabels.NAME).GetComponentInChildren<Text>();
            var avatarImage = i.Find(UILabels.AVATAR).GetComponent<Image>();
            textField.text = cName;
            avatarImage.sprite = avatar;
        }
    }
}