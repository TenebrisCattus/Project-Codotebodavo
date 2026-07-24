using UnityEngine;

public class EnemySleepMeleeScript : EnemyScript
{
    private bool isTouched;

    private void Start()
    {
        SetRB(GetComponent<Rigidbody2D>());
    }
    private void Update()
    {
        FindPlayerRightAndDestinaton();
        if (isTouched)
        {
            GoToPlayer();
        }
        else if (GetHP() < GetMaxHP())
        {
            OnTouched();
        }
    }
    private void GoToPlayer()
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

    public override void OnTouched()
    {
        base.OnTouched();
        isTouched = true;
    }
}
