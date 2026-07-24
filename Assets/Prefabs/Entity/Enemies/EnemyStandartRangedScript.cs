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
        if (SeePlayer() && DestinatonToPlayer() < ReactionRadius() && Time.time > nextTimeForAttackRanged)
        {
            Instantiate(Projectile, transform.position, transform.rotation).GetComponent<ProjectileScript>().SetStartConditions(1000, 10, ProjectileSprite);
            nextTimeForAttackRanged = Time.time + GetAttackDelay();
        }
    }

    private void Flip()
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
}
