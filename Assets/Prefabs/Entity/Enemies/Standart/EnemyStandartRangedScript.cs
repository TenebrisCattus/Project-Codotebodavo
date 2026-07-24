using UnityEngine;

public class EnemyStandartRangedScript : EnemyScript
{
    [Header("Projectile Settings")]
    [SerializeField] GameObject Projectile;
    [SerializeField] Sprite ProjectileSprite;
    [SerializeField] float damage;

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
        if (SeePlayer() && DestinatonToPlayer() < ReactionRadius() && Time.time > nextTimeForAttackRanged)
        {
            Instantiate(Projectile, transform.position, transform.rotation).GetComponent<ProjectileScript>().SetStartConditions(1000, 10, ProjectileSprite, damage);
            nextTimeForAttackRanged = Time.time + GetAttackDelay();
        }
    }
}
