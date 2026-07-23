using UnityEngine;

public class PlayerScript : EntityScript
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
    [SerializeField] private float airModifire;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float currentHorisontalInput;
    private bool isGrounded;
    private Transform GroundTransform;
    private float accelerationDemodifire;

    public static PlayerScript Game_player { get; private set; }

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
        if (isGrounded)
        {
            accelerationDemodifire = 1;
        }
        else 
        { 
            accelerationDemodifire = airModifire;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < currentHorisontalInput)
        {
            currentHorisontalInput -= Mathf.Min(acceleration/accelerationDemodifire, currentHorisontalInput - horizontalInput);
        } else if (horizontalInput > currentHorisontalInput)
        {
            currentHorisontalInput += Mathf.Min(acceleration/accelerationDemodifire, horizontalInput - currentHorisontalInput);
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
        Debug.Log(currentHorisontalInput * moveSpeed);
    }
}