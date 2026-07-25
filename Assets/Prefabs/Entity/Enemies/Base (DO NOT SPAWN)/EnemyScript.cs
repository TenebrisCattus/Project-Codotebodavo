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

    private float nextTimeForAttack;
    private float destinatonToPlayerX;
    private bool isPlayerRight;
    private float speed;
    private float damage;

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
        speed = 0;
        damage = 0;
        Invoke("StopStun", stunLenght);
    }

    private void StopStun()
    {
        speed = standartSpeed;
        damage = standartDamage;
    }

    public virtual void OnTouched() { }
}
