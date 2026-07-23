using UnityEngine;

public class PlayerController : EntityScript
{
    [Header("Ссылки на внутренние объекты")]
    [SerializeField] private GameObject groundCheck;
    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float acceleration;
    [Header("Настройки проверки земли")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float currentHorisontalInput;
    private bool isGrounded;
    private Transform GroundTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GroundTransform = groundCheck.transform;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < currentHorisontalInput)
        {
            currentHorisontalInput -= Mathf.Min(acceleration, currentHorisontalInput - horizontalInput);
        } else if (horizontalInput > currentHorisontalInput)
        {
            currentHorisontalInput += Mathf.Min(acceleration, horizontalInput - currentHorisontalInput);
        }
            isGrounded = Physics2D.OverlapCircle(GroundTransform.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(currentHorisontalInput * moveSpeed, rb.linearVelocity.y);
    }
}