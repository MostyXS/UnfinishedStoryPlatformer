using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu.UI
{
    [RequireComponent(typeof(Button))]
    public class TransformSelectButton : MonoBehaviour
    {
        [SerializeField] private GameObject objectToActivate;

        private Button _thisButton;

        private void Awake()
        {
            _thisButton = GetComponent<Button>();
            _thisButton.onClick.AddListener(SelectObject);
        }

        private void SelectObject()
        {
            foreach (Transform c in objectToActivate.transform.parent)
            {
                c.gameObject.SetActive(c.gameObject == objectToActivate);
            }
        }
    }
}