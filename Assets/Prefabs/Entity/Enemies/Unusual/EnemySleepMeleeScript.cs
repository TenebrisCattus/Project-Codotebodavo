using UnityEngine;

public class EnemySleepMeleeScript : EnemyStandartMeleeScript
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

    public override void OnTouched()
    {
        base.OnTouched();
        isTouched = true;
    }
}
