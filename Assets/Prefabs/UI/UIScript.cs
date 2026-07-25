using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image brain;
    [SerializeField] private Image heathBarUpper;
    [SerializeField] private Image heathBarUnder;
    [SerializeField] private Image bulletIcon;
    [SerializeField] private TextMeshProUGUI ammo;
    [SerializeField] private Image CountHun;
    [SerializeField] private Image CountDec;
    [SerializeField] private Image CountNum;
    [Header("Sprites (CountDown)")]
    [SerializeField] private Sprite Count_none;
    [SerializeField] private Sprite Count_0;
    [SerializeField] private Sprite Count_1;
    [SerializeField] private Sprite Count_2;
    [SerializeField] private Sprite Count_3;
    [SerializeField] private Sprite Count_4;
    [SerializeField] private Sprite Count_5;
    [SerializeField] private Sprite Count_6;
    [SerializeField] private Sprite Count_7;
    [SerializeField] private Sprite Count_8;
    [SerializeField] private Sprite Count_9;
    [Header("Sprites (Brain)")]
    [SerializeField] private Sprite Brain_0;
    [SerializeField] private Sprite Brain_1;
    [SerializeField] private Sprite Brain_2;
    [SerializeField] private Sprite Brain_3;
    [SerializeField] private Sprite Brain_4;
    [Header("Sprites (Flat Bullet)")]
    [SerializeField] private Sprite FlBullet_0;
    [SerializeField] private Sprite FlBullet_1;
    [SerializeField] private Sprite FlBullet_2;
    [SerializeField] private Sprite FlBullet_3;
    [SerializeField] private Sprite FlBullet_4;
    [Header("Sprites (Shell)")]
    [SerializeField] private Sprite Shell_0;
    [SerializeField] private Sprite Shell_1;
    [SerializeField] private Sprite Shell_2;
    [SerializeField] private Sprite Shell_3;
    [SerializeField] private Sprite Shell_4;
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
    private int count;
    private int brainFrame = 0;
    void Start()
    {
        MaxHP = PlayerScript.Game_player.GetMaxHP();
        BrainAnim();
        BulletAnim();
    }

    void Update()
    {
        SetAllVarFromPlayer();
        HealthUpdate();
        if (ActivateUnder) { HealthDownUpdate(); }
        SetAmmoText();
        SetCount();
    }

    private void SetAllVarFromPlayer()
    {
        currentRealHP = PlayerScript.Game_player.GetHP()/MaxHP;
        currentWeapon = PlayerScript.Game_player.GetCurrentWeapon();
        ammoes = PlayerScript.Game_player.EveryAmmo();
        count = PlayerScript.Game_player.GetTimer();
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

    private void SetCount()
    {
        if (count > 99)
        {
            SetImageTimer(count.ToString()[0], CountHun);
            SetImageTimer(count.ToString()[1], CountDec);
            SetImageTimer(count.ToString()[2], CountNum);
        }
        else if (count > 9)
        {
            SetImageTimer('n', CountHun);
            SetImageTimer(count.ToString()[0], CountDec);
            SetImageTimer(count.ToString()[1], CountNum);
        }
        else
        {
            SetImageTimer('n', CountHun);
            SetImageTimer('n', CountDec);
            SetImageTimer(count.ToString()[0], CountNum);
        }
    }

    private void SetImageTimer(char num, Image coun) 
    {
        switch (num) 
        {
            case ('0'):
                coun.sprite = Count_0;
                break;
            case ('1'):
                coun.sprite = Count_1;
                break;
            case ('2'):
                coun.sprite = Count_2;
                break;
            case ('3'):
                coun.sprite = Count_3;
                break;
            case ('4'):
                coun.sprite = Count_4;
                break;
            case ('5'):
                coun.sprite = Count_5;
                break;
            case ('6'):
                coun.sprite = Count_6;
                break;
            case ('7'):
                coun.sprite = Count_7;
                break;
            case ('8'):
                coun.sprite = Count_8;
                break;
            case ('9'):
                coun.sprite = Count_9;
                break;
            default:
                coun.sprite = Count_none;
                break;
        }
    }

    private void BrainAnim()
    {
        if (brainFrame > 4) { brainFrame = 0; }
        switch (brainFrame)
        {
            case (0):
                brain.sprite = Brain_0;
                break;
                case (1):
                brain.sprite = Brain_1;
                break;
                case (2):
                brain.sprite = Brain_2;
                break;
                case (3):
                brain.sprite = Brain_3;
                break;
                case (4):
                brain.sprite = Brain_4;
                break;
        }
        brainFrame++;
        Invoke("BrainAnim", 0.2f);
    }

    private void BulletAnim()
    {
        switch (currentWeapon)
        {
            case "Wep_Pistol":
                switch (brainFrame)
                {
                    case (0):
                        bulletIcon.sprite = FlBullet_0;
                        break;
                    case (1):
                        bulletIcon.sprite = FlBullet_1;
                        break;
                    case (2):
                        bulletIcon.sprite = FlBullet_2;
                        break;
                    case (3):
                        bulletIcon.sprite = FlBullet_3;
                        break;
                    case (4):
                        bulletIcon.sprite = FlBullet_4;
                        break;
                }
                break;
            case "Wep_SMG":
                bulletIcon.sprite = null;
                break;
            case "Wep_BMG":
                bulletIcon.sprite = null;
                break;
            case "Wep_Shotgun":
                switch (brainFrame)
                {
                    case (0):
                        bulletIcon.sprite = Shell_0;
                        break;
                    case (1):
                        bulletIcon.sprite = Shell_1;
                        break;
                    case (2):
                        bulletIcon.sprite = Shell_2;
                        break;
                    case (3):
                        bulletIcon.sprite = Shell_3;
                        break;
                    case (4):
                        bulletIcon.sprite = Shell_4;
                        break;
                }
                break;
            case "none":
                bulletIcon.sprite = null;
                break;
        }
        Invoke("BulletAnim", 0.2f);
    }
}
