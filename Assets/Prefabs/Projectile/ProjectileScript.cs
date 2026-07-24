using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float projectileSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float lifetime;
    private float damage;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        
    }

    public void SetStartConditions(float speed, float lifetime, Sprite sprite, float damage)
    {
        projectileSpeed = speed;
        sr.sprite = sprite;
        rb.AddForce(transform.right * projectileSpeed * -1);
        this.lifetime = lifetime;
        this.damage = damage;
        Destroy(gameObject, lifetime);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("Enemy"))
        {
            hitObject.GetComponent<EntityScript>().GiveDamage(damage);
        }
        Destroy(gameObject, 0);
    }
} 
