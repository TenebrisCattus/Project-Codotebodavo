using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image heathBarUpper;
    [SerializeField] private Image heathBarUnder;
    [SerializeField] private Image bulletIcon;
    [SerializeField] private TextMeshProUGUI ammo;
    [Header("Settings")]
    [SerializeField] private float healthBarSpeed;
    [SerializeField] private float delayForUnder;

    private float currentShowUpHP;
    private float currentShowDownHP;
    private float currentRealHP;
    private float MaxHP;
    private bool HPChanged;
    private bool ActivateUnder;
    private string currentWeapon;
    private int[] ammoes;
    void Start()
    {
        MaxHP = PlayerScript.Game_player.GetMaxHP();
    }

    void Update()
    {
        SetAllVarFromPlayer();
        HealthUpdate();
        if (ActivateUnder) { HealthDownUpdate(); }
        SetAmmoText();
    }

    private void SetAllVarFromPlayer()
    {
        currentRealHP = PlayerScript.Game_player.GetHP()/MaxHP;
        currentWeapon = PlayerScript.Game_player.GetCurrentWeapon();
        ammoes = PlayerScript.Game_player.EveryAmmo();
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

    private void SetAmmoText()
    {
        switch (currentWeapon)
        {
            case "Wep_Pistol":
                ammo.text = ammoes[0].ToString() + "/10";
                break;
            case "Wep_SMG":
                ammo.text = ammoes[1].ToString() + "/30";
                break;
            case "Wep_BMG":
                ammo.text = ammoes[3].ToString() + "/1";
                break;
            case "Wep_Shotgun":
                ammo.text = ammoes[2].ToString() + "/2";
                break;
            case "none":
                ammo.text = "--/--";
                break;
        }
    }

}
