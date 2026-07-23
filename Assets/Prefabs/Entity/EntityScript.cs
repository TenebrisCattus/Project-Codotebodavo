using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    [Header("Çהמנמגüו")]
    [SerializeField] private float HP = 1.0f;
    [SerializeField] private float MaxHP = 1.0f;

    void Start()
    {
        
    }

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

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
