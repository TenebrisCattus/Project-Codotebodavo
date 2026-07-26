using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : EntityScript
{
    [Header("Links to internal objects")]
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject caseWeapon;
    [SerializeField] private GameObject Torso;
    [SerializeField] private GameObject Arm;
    [SerializeField] private GameObject Legs;
    [SerializeField] private GameObject Effect;
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float acceleration;
    [Header("Ground check settings")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float airModifire;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Sprite ProjectileSprite;
    [Header("Damage settings")]
    [SerializeField] private float invulnerability;
    [SerializeField] private float stun;
    [Header("Item settings")]
    [SerializeField] private float energyDelay;
    [SerializeField] private float energyTimeSave;
    [SerializeField] private float energyCost;
    [SerializeField] private float restDelay;
    [SerializeField] private float restHP;
    [SerializeField] private int restCost;
    [SerializeField] private float CoolDown;

    private float timerPeriod = 1;
    private float horizontalInput;
    private float UpDownSightInput;
    private float LeftRightSightInput;
    private bool RightSight = true;
    private float currentHorisontalInput;
    private bool isGrounded;
    private Transform GroundTransform;
    private float accelerationDemodifire;
    private float sightDirection;
    private float moveblock;
    private bool attackblockRoEUsed;
    private bool canRestOrEnergy;
    private float PistolFireDelay;
    private float SmgFireDelay;
    private float ShotgunFireDelay;
    private float BMGFireDelay;
    private string CurrectWeapon = "none";
    private Animator TorsoAnim;
    private Animator LegsAnim;
    private Animator EffectAnim;
    private Animator ArmAnim;
    private Vector3 currentRotation;
    private float caseTimer;

    private string currentLocName;
    public string nextLocName;

    private int PistolAmmo = 10;
    private int SMGAmmo = 30;
    private int ShotgunAmmo = 2;
    private int BMGAmmo = 1;

    private int timer = 999;

    public static PlayerScript Game_player { get; private set; }

    private void Awake()
    {
        if (Game_player == null)
        {
            Game_player = GetComponent<PlayerScript>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TorsoAnim = Torso.GetComponent<Animator>();
        ArmAnim = Arm.GetComponent<Animator>();
        LegsAnim = Legs.GetComponent<Animator>();
        EffectAnim = Effect.GetComponent<Animator>();
        attackblockRoEUsed = false;
        canRestOrEnergy = true;
        SetRB(GetComponent<Rigidbody2D>());
        GroundTransform = groundCheck.transform;
        Invoke("CountDownTheTimer", timerPeriod);
    }

    private void Update()
    {
        MovementUpdate();
        BattleUpdate();
        CaseAttack();
        ItemManager();
        RightSidePositionManager();
    }
    public string GetCurrectWeapon()
    {
        return CurrectWeapon;
    }

    private void FixedUpdate()
    {
        if (moveblock == 0 && !attackblockRoEUsed) { GetRB().linearVelocity += new Vector2(horizontalInput * acceleration * 0.1f, 0); }
        if (GetRB().linearVelocity.x > moveSpeed)
        {
            GetRB().linearVelocity = new Vector2(moveSpeed, GetRB().linearVelocity.y);
        }else if (GetRB().linearVelocity.x < moveSpeed * -1)
        {
            GetRB().linearVelocity = new Vector2(moveSpeed * -1, GetRB().linearVelocity.y);
        }
        if (horizontalInput == 0 && isGrounded)
        {
            GetRB().linearVelocity = new Vector2(GetRB().linearVelocity.x/1.1f, GetRB().linearVelocity.y);
        }
    }

    public string GetCurrentWeapon() { return CurrectWeapon; }

    public int[] EveryAmmo()
    {
        return new int[] { PistolAmmo, SMGAmmo, ShotgunAmmo, BMGAmmo };
    }

    private void BattleUpdate()
    {
        if ((Input.GetAxisRaw("Fire1")) == 1 && !attackblockRoEUsed)
        {
            if (Time.time >= PistolFireDelay && CurrectWeapon == "Wep_Pistol" && PistolAmmo > 0)
            {
                PistolFireDelay = Time.time + 0.5f;
                PistolAmmo -= 1;
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.45f, 0, false);
            }
            else if (Time.time >= SmgFireDelay && CurrectWeapon == "Wep_SMG" && SMGAmmo > 0)
            {
                SmgFireDelay = Time.time + 0.1f;
                SMGAmmo -= 1;
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.30f, 0, false);
            }
            else if (CurrectWeapon == "Wep_BMG" && BMGAmmo > 0)
            {
                BMGAmmo -= 1;
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 2.25f, 0, false);
            }
            else if (Time.time >= ShotgunFireDelay && CurrectWeapon == "Wep_Shotgun" && ShotgunAmmo > 0)
            {
                ShotgunAmmo -= 1;
                ShotgunFireDelay = Time.time + 0.5f;
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection + 12f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection + 9f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection + 6f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection + 3f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection - 3f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection - 6f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection - 9f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);
                Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection - 12f)).GetComponent<ProjectileScript>().SetStartConditions(3000, 2, ProjectileSprite, 0.14f, 0, false);

            }
        }
        if ((Input.GetAxisRaw("WeaponDrop")) == 1 && CurrectWeapon != "none")
        {
            ArmAnim.SetTrigger("Throw");
            CurrectWeapon = "none";
            SmgFireDelay = 0;
            PistolFireDelay = 0;
            ShotgunFireDelay = 0;
            PistolAmmo = 10;
            SMGAmmo = 30;
            ShotgunAmmo = 2;
            BMGAmmo = 1;
    Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0, 0, sightDirection)).GetComponent<ProjectileScript>().SetStartConditions(3000, 5, ProjectileSprite, 0, 3f, false);
        }
    }

    public void SetWeapon(String weapon)
    {
        CurrectWeapon = weapon;
    }
    private void MovementUpdate()
    {
        if (Mathf.Abs(GetRB().linearVelocity.x) > 0.1f)
        {
            LegsAnim.SetBool("Run", true);
            TorsoAnim.SetBool("Run", true);
        }
        else
        {
            LegsAnim.SetBool("Run", false);
            TorsoAnim.SetBool("Run", false);
        }
        if (isGrounded)
        {
            accelerationDemodifire = 1;
        }
        else
        {
            accelerationDemodifire = airModifire;
        }
        moveblock = Input.GetAxisRaw("Moveblock");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (moveblock == 0)
        {
            currentHorisontalInput -= Mathf.Min(acceleration / accelerationDemodifire, currentHorisontalInput - horizontalInput);
        }
        else if (moveblock == 0)
        {
            currentHorisontalInput += Mathf.Min(acceleration / accelerationDemodifire, horizontalInput - currentHorisontalInput);
        }
        // Блок, запрещающий движение при нажатой кнопки лока. Если это не нужно - закомментируйте этот if
        if (moveblock == 1 && isGrounded)
        {
            currentHorisontalInput = 0;
        }
        isGrounded = Physics2D.OverlapCircle(GroundTransform.position, groundCheckRadius, groundLayer);
        LegsAnim.SetBool("OnGround", isGrounded);
        TorsoAnim.SetBool("OnGround", isGrounded);
        ArmAnim.SetBool("OnGround", isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded && !attackblockRoEUsed)
        {
            LegsAnim.SetTrigger("Jump");
            TorsoAnim.SetTrigger("Jump");
            ArmAnim.SetTrigger("Jump");
            Invoke("Jump", 0.2f);
        }

        if (horizontalInput == -1)
        {
            RightSight = false;
        }
        else if (horizontalInput == 1)
        {
            RightSight = true;
        }

        UpDownSightInput = Input.GetAxisRaw("Vertical");
        LeftRightSightInput = Input.GetAxisRaw("Horizontal");

        switch (UpDownSightInput, LeftRightSightInput)
        {
            default:
                if (RightSight)
                {
                    sightDirection = 180f;
                    ArmAnim.SetInteger("Direction", 6);
                }
                else if (!RightSight)
                {
                    sightDirection = 0f;
                    ArmAnim.SetInteger("Direction", 1);
                }
                break;
            case (0, 1):
                sightDirection = 180f;
                ArmAnim.SetInteger("Direction", 6);
                break;
            case (0, -1):
                sightDirection = 0f;
                ArmAnim.SetInteger("Direction", 1);
                break;
            case (-1, 0):
                sightDirection = 90f;
                if (RightSight)
                {
                    ArmAnim.SetInteger("Direction", 4);
                }
                else if (!RightSight)
                {
                    ArmAnim.SetInteger("Direction", 3);
                }
                break;
            case (1, 0):
                sightDirection = 270f;
                if (RightSight)
                {
                    ArmAnim.SetInteger("Direction", 8);
                }
                else if (!RightSight)
                {
                    ArmAnim.SetInteger("Direction", 9);
                }
                break;
            case (1, 1):
                sightDirection = 225f;
                ArmAnim.SetInteger("Direction", 7);
                break;
            case (-1, -1):
                sightDirection = 45f;
                ArmAnim.SetInteger("Direction", 2);
                break;
            case (1, -1):
                sightDirection = 315f;
                ArmAnim.SetInteger("Direction", 10);
                break;
            case (-1, 1):
                sightDirection = 135f;
                ArmAnim.SetInteger("Direction", 5);
                break;
        }
    }
    
    private void Jump()
    {
        GetRB().linearVelocity = new Vector2(GetRB().linearVelocity.x, jumpForce);
    }
    private void CountDownTheTimer()
    {
        timer--;
        if (timer == 0)
        {
            Death();
        }
        Invoke("CountDownTheTimer", timerPeriod);
    }

    public void SetTimer(int timer)
    {
        this.timer = timer;
    }

    public int GetTimer()
    {
        return timer;
    }

    public override void GiveDamage(float damage)
    {
        base.GiveDamage(damage);
        ActivateInvulnerability(invulnerability);

    }

    private void ActivateInvulnerability(float invulnerability)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Invoke("DeactivateInvulnerability", invulnerability);
    }
    private void DeactivateInvulnerability()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    private void CaseAttack()
    {
        if (Input.GetAxisRaw("CaseAttack") == 1 && !attackblockRoEUsed && Time.time > caseTimer)
        {
            caseTimer = Time.time+1;
            Arm.GetComponent<SpriteRenderer>().enabled = false;
            TorsoAnim.SetTrigger("CaseAttack");
            EffectAnim.SetTrigger("Start");
            if (caseWeapon.GetComponent<CaseScript>().InTrigger().Count > 0)
            {
                foreach (Collider2D col in caseWeapon.GetComponent<CaseScript>().InTrigger())
                {
                    if (col.gameObject.CompareTag("Enemy"))
                    {
                        col.gameObject.GetComponent<EnemyScript>().Stun(stun);
                    }
                }
            }
            Invoke("ArmShow", 0.8f);
        }
    }

    private void ArmShow()
    {
        Arm.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void ItemManager()
    {
        if (Input.GetAxisRaw("Energy") == 1 && canRestOrEnergy)
        {
            attackblockRoEUsed = true;
            canRestOrEnergy = false;
            EnergyUse();
            Invoke("ItemCooldownEnd", CoolDown);
        }
        else if (Input.GetAxisRaw("Heal") == 1 && canRestOrEnergy)
        {
            attackblockRoEUsed = true;
            canRestOrEnergy = false;
            RestUse();
            Invoke("ItemCooldownEnd", CoolDown);
        }
    }

    private void ItemCooldownEnd()
    {
        canRestOrEnergy = true;
    }

    private void EnergyUse()
    {
        Arm.GetComponent<SpriteRenderer>().enabled = false;
        TorsoAnim.SetTrigger("Energy");
        timerPeriod = 10;
        ActivateInvulnerability(energyDelay);
        Invoke("EnergyUseEnd", energyDelay);
    }

    private void EnergyUseEnd()
    {
        Arm.GetComponent<SpriteRenderer>().enabled = true;
        GiveDamage(energyCost);
        attackblockRoEUsed = false;
        Invoke("EnergyEffectEnd", energyTimeSave-energyDelay);
    }

    private void EnergyEffectEnd()
    {
        timerPeriod = 1;
    }

    private void RestUse()
    {
        Arm.GetComponent<SpriteRenderer>().enabled = false;
        timer -= restCost;
        TorsoAnim.SetTrigger("Breath");
        ActivateInvulnerability(restDelay);
        Invoke("RestUseEnd", restDelay);
    }

    private void RestUseEnd()
    {
        Arm.GetComponent<SpriteRenderer>().enabled = true;
        GiveDamage(restHP * -1);
        attackblockRoEUsed = false;
    }

    private void RightSidePositionManager()
    {
        if (RightSight)
        {
            caseWeapon.transform.localPosition = new Vector3(1.25f, caseWeapon.transform.localPosition.y, caseWeapon.transform.localPosition.z);
            TorsoAnim.SetBool("Right", true);
            LegsAnim.SetBool("Right", true);
            EffectAnim.SetBool("Right", true);
            currentRotation = Torso.transform.eulerAngles;
            currentRotation.y = 0f;
            Torso.transform.eulerAngles = currentRotation;
            currentRotation = Legs.transform.eulerAngles;
            currentRotation.y = 0f;
            Legs.transform.eulerAngles = currentRotation;
        }
        else
        {
            caseWeapon.transform.localPosition = new Vector3(-1.25f, caseWeapon.transform.localPosition.y, caseWeapon.transform.localPosition.z);
            TorsoAnim.SetBool("Right", false);
            LegsAnim.SetBool("Right", false);
            EffectAnim.SetBool("Right", false);
            currentRotation = Torso.transform.eulerAngles;
            currentRotation.y = 180f;
            Torso.transform.eulerAngles = currentRotation;
            currentRotation = Legs.transform.eulerAngles;
            currentRotation.y = 180f;
            Legs.transform.eulerAngles = currentRotation;
        }
    }

    public override void Death()
    {
        SceneManager.LoadScene(currentLocName);
    }

    public void SetLocs(string cur, string newLoc) { 
        currentLocName = cur;
        nextLocName = newLoc;
    }
}