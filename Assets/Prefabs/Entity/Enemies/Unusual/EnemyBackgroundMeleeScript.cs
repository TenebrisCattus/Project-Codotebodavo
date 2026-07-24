using UnityEngine;

public class EnemyBackgroundMeleeScript : EnemyStandartMeleeScript
{
    private bool isActive = false;
    private float standartGravity;
    private void Start()
    {
        SetRB(GetComponent<Rigidbody2D>());
        SetCollider(GetComponent<Collider2D>());
        standartGravity = GetRB().gravityScale;
        GetRB().gravityScale = 0;
        GetCollider().enabled = false;
    }
    private void Update()
    {
        FindPlayerRightAndDestinaton();
        if (isActive) 
        {
            GoToPlayer();
        }
        else if (IsPlayerRight())
        {
            isActive = true;
            GetRB().gravityScale = standartGravity;
            GetCollider().enabled = true;
        }
    }
}
