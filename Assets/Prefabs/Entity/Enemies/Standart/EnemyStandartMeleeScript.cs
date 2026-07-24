using UnityEngine;

public class EnemyStandartMeleeScript : EnemyScript
{

    private void Start()
    {
        SetRB(GetComponent<Rigidbody2D>());
    }
    private void Update()
    {
        FindPlayerRightAndDestinaton();
        GoToPlayer();
    }

    public void GoToPlayer()
    {
        if (SeePlayer() && DestinatonToPlayer() < ReactionRadius())
        {
            if (IsPlayerRight())
            {
                GetRB().linearVelocityX = GetSpeed();
            }
            else
            {
                GetRB().linearVelocityX = GetSpeed() * -1;
            }
        }
    }
}
