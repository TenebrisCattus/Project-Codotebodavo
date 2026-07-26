using UnityEngine;

public class EnemyStandartRangedScript : EnemyScript
{
    [Header("Projectile Settings")]
    [SerializeField] GameObject Projectile;
    [SerializeField] Sprite ProjectileSprite;

    private float nextTimeForAttackRanged;

    private void Update()
    {
        Flip();
        FindPlayerRightAndDestinaton();
        Fire();
    }

    public void Flip()
    {
        if (IsPlayerRight())
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -180f, transform.eulerAngles.z);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }
    public void Fire()
    {
        if (SeePlayer() && DestinatonToPlayer() < ReactionRadius() && Time.time > nextTimeForAttackRanged && !GetStunned())
        {
            Instantiate(Projectile, transform.position, transform.rotation).GetComponent<ProjectileScript>().SetStartConditions(3000, 10, ProjectileSprite, GetDamage(), 0, true);
            nextTimeForAttackRanged = Time.time + GetAttackDelay();
        }
    }
}
