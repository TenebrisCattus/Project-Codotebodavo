using UnityEngine;

public class EnemyStandartMeleeScript : EnemyScript
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        FindPlayerRightAndDestinaton();
        if (SeePlayer() && DestinatonToPlayer() < ReactionRadius())
        {
            if (IsPlayerRight())
            {
                rb.linearVelocityX = GetSpeed();
            }
            else
            {
                rb.linearVelocityX = GetSpeed() * -1;
            }
        }
    }
}
