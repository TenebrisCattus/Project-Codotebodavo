using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image heathBarUpper;
    [SerializeField] private Image heathBarUnder;
    [Header("Settings")]
    [SerializeField] private float healthBarSpeed;
    [SerializeField] private float delayForUnder;

    private float currentShowUpHP;
    private float currentShowDownHP;
    private float currentRealHP;
    private float MaxHP;
    private bool HPChanged;
    private bool ActivateUnder;
    void Start()
    {
        
    }

    void Update()
    {
        currentRealHP = PlayerScript.Game_player.GetHP();
        MaxHP = PlayerScript.Game_player.GetMaxHP();
        HealthUpdate();
        if (ActivateUnder) { HealthDownUpdate(); }
    }

    private void HealthUpdate()
    {
        heathBarUpper.fillAmount = currentShowUpHP;
        if (currentShowUpHP < currentRealHP)
        {
            currentShowUpHP += Mathf.Min(healthBarSpeed, currentRealHP - currentShowUpHP);
            HPChanged = true;
        }
        else if (currentShowUpHP > currentRealHP)
        {
            currentShowUpHP -= Mathf.Min(healthBarSpeed, currentShowUpHP - currentRealHP);
            HPChanged = true;
        }
        else if (HPChanged)
        {
            Invoke("Activate", delayForUnder);
            HPChanged = false;
        }
    }

    private void Activate()
    {
        ActivateUnder = true;
    }

    private void HealthDownUpdate()
    {
        heathBarUnder.fillAmount = currentShowDownHP;
        if (currentShowDownHP < currentRealHP)
        {
            currentShowDownHP += Mathf.Min(healthBarSpeed, currentRealHP - currentShowDownHP);
        }
        else if (currentShowDownHP > currentRealHP)
        {
            currentShowDownHP -= Mathf.Min(healthBarSpeed, currentShowDownHP - currentRealHP);
        }
        else
        {
            ActivateUnder = false;
        }
    }
}
