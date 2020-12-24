using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogues.Components
{
    public class DialogueCharacter : ScriptableObject, IInsertable
    {
        
        [SerializeField] string cName;
        [SerializeField] Sprite avatar;
        public void InsertInInterface(Transform i)
        {
            var textField = i.Find(UILabels.NAME).GetComponentInChildren<Text>();
            var avatarImage = i.Find(UILabels.AVATAR).GetComponentInChildren<Image>();
            textField.text = cName;
            avatarImage.sprite = avatar;
        }
    }
}