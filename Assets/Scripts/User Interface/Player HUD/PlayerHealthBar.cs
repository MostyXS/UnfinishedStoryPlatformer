using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Attributes.UI
{
    //TODO access directly from UI and not from player
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] GameObject healthDisplayPrefab;

        private Health _playerHealth;
        private Image _bar;

        private void Awake()
        {
            _playerHealth = GetComponent<Health>();
        }

        private void Start()
        {
            _playerHealth.onHealthChange += ChangeFillAmount;
            _bar = Instantiate(healthDisplayPrefab, MainCanvas.Instance).GetComponent<Image>();
            _bar.fillAmount = 1f;
            ChangeFillAmount();
        }

        private void ChangeFillAmount()
        {
            _bar.fillAmount = _playerHealth.GetHealthPercentage();
        }
    }
}