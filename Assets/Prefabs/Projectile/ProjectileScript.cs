using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float projectileSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float lifetime;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStartConditions(float speed, float lifetime, Sprite sprite)
    {
        projectileSpeed = speed;
        sr.sprite = sprite;
        rb.AddForce(transform.right * projectileSpeed * -1);
        this.lifetime = lifetime;
    }
}
