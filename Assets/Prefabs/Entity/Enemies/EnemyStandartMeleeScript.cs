using UnityEngine;

public class EnemyStandartMeleeScript : EnemyScript
{
    private void Update()
    {
        if (SeePlayer())
        {
            if (IsPlayerRight())
            {
                GetSpeed();
            }
        }
    }
}
