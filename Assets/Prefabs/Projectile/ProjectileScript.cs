using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float projectileSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float damage;
    private float stun;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        
    }

    public void SetStartConditions(float speed, float lifetime, Sprite sprite, float damage, float stun)
    {
        projectileSpeed = speed;
        sr.sprite = sprite;
        rb.AddForce(transform.right * projectileSpeed * -1);
        this.damage = damage;
        this.stun = stun;
        Destroy(gameObject, lifetime);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("Enemy"))
        {
            hitObject.GetComponent<EntityScript>().GiveDamage(damage);
            hitObject.GetComponent<EnemyScript>().Stun(stun);
        }
        Destroy(gameObject, 0);
    }
} 
