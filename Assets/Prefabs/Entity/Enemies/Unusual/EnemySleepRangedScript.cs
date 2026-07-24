using UnityEngine;

public class EnemySleepRangedScript : EnemyStandartRangedScript
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
            Flip();
            Fire();
        }
    }

    public override void OnTouched()
    {
        base.OnTouched();
        isTouched = true;
    }
}
