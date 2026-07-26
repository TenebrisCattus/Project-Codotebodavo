using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class EnemyScript : EntityScript
{
    [Header("AI Settings")]
    [SerializeField] private float reactionRadius;
    [SerializeField] private bool dealDamageWhenTouched;
    [SerializeField] private float attackDelay;
    [SerializeField] private float standartDamage;
    [Header("Movement Settings")]
    [SerializeField] private float standartSpeed;
    [Header("Obstacle Layer")]
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Loot")]
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject SMG;
    [SerializeField] private GameObject Shotgun;
    [SerializeField] private GameObject BMG;

    private float nextTimeForAttack;
    private float destinatonToPlayerX;
    private bool isPlayerRight;
    private float speed;
    private float damage;
    private bool stunned;
    private bool hasweapon = true;


    private void Awake()
    {
        speed = standartSpeed;
        damage = standartDamage;
    }

    private void Update()
    {
        FindPlayerRightAndDestinaton();
    }
    public void FindPlayerRightAndDestinaton()
    {
        destinatonToPlayerX = PlayerScript.Game_player.transform.position.x - transform.position.x;
        if (destinatonToPlayerX > 0)
        {
            isPlayerRight = true;
        }
        else
        {
            isPlayerRight = false;
        }
    }

    public bool SeePlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, PlayerScript.Game_player.transform.position, obstacleLayer);
        if (hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float DestinatonToPlayer()
    {
        return Mathf.Abs(destinatonToPlayerX);
    }

    public bool IsPlayerRight() 
    {
        return isPlayerRight;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float ReactionRadius() {  return reactionRadius; }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dealDamageWhenTouched && Time.time > nextTimeForAttack)
        {
            PlayerScript.Game_player.GiveDamage(damage);
            nextTimeForAttack = Time.time + attackDelay;
            OnTouched();
        }
    }

    public float GetAttackDelay()
    {
        return attackDelay;
    }

    public void Stun(float stunLenght)
    {
        if (stunLenght > 0)
        {
            stunned = true;
            speed = 0;
            damage = 0;
            if (hasweapon)
            {
                hasweapon = false;
                DropLoot();
            }
            Invoke("StopStun", stunLenght);
        }
        
    }

    private void StopStun()
    {
        stunned = false;
        speed = standartSpeed;
        damage = standartDamage;
    }

    public bool GetStunned()
    {
        return stunned;
    }

    public float GetDamage()
    {
        return damage;
    }
    public void DropLoot()
    {

        int lootChance = Random.Range(21, 100);

        if (lootChance > 20 && lootChance <= 50)
        {
            Instantiate(Pistol, transform.position, transform.rotation);
        }
        else if (lootChance > 50 && lootChance <= 80)
        {
            int lootChance2 = Random.Range(0, 1);
            if (lootChance2 == 0)
            {
                Instantiate(SMG, transform.position, transform.rotation);
            }
            else if (lootChance2 == 1)
            {
                Instantiate(Shotgun, transform.position, transform.rotation);
            }        
        }
        else if (lootChance > 80)
        {
            Instantiate(BMG, transform.position, transform.rotation);
        }
    }
    public virtual void OnTouched() { }
}
