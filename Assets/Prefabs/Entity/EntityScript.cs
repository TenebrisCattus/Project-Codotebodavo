using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float HP = 1.0f;
    [SerializeField] private float MaxHP = 1.0f;

    private Rigidbody2D rb;
    private Collider2D collision;

    void Update()
    {
        
    }

    public float GetHP() 
    { 
        return HP;
    }

    public void SetHP(float HP)
    {
        this.HP = HP;
        if (HP < 0) 
        {
            Death();
        }
        if (HP > MaxHP) 
        { 
            HP = MaxHP;
        }
    }

    public float GetMaxHP()
    {
        return MaxHP;
    }

    //ѕри переписывании GiveDamage в рамках дочернего класса использовать SetHP ќЅя«ј“≈Ћ№Ќќ
    public virtual void GiveDamage(float damage)
    {
        SetHP(GetHP() - damage);
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public Rigidbody2D GetRB() {  return rb; }
    public void SetRB(Rigidbody2D rb) { this.rb = rb; }
    public Collider2D GetCollider() { return collision; }
    public void SetCollider(Collider2D collision) { this.collision = collision; }
}
