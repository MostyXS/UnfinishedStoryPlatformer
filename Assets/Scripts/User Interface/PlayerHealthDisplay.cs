using Game.Combat.Common;
using MostyProUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject healthDisplayPrefab;

    Health playerHealth;
    Image bar;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        
        
    }
    private void Start()
    {
        playerHealth.onHealthChange += ChangeFillAmount;
        bar = Instantiate(healthDisplayPrefab, MainCanvas.Transform).GetComponent<Image>();
        bar.fillAmount = 1f;
        ChangeFillAmount();
        
    }
    private void ChangeFillAmount()
    {
        bar.fillAmount = playerHealth.GetHealthPercentage();
    }

    

}
