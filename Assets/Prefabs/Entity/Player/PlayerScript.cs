using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerScript : EntityScript
{
    [Header("Links to internal objects")]
    [SerializeField] private GameObject groundCheck;
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float acceleration;
    [Header("Ground check settings")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float airModifire;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Sprite ProjectileSprite;


    private Rigidbody2D rb;
    private float horizontalInput;
    private float UpDownSightInput;
    private float LeftRightSightInput;
    private bool RightSight = true;
    private float currentHorisontalInput;
    private bool isGrounded;
    private Transform GroundTransform;
    private float accelerationDemodifire;
    private float sightDirection;
    private float moveblock;

    public static PlayerScript Game_player { get; private set; }

    private void Awake()
    {
        if (Game_player == null)
        {
            Game_player = GetComponent<PlayerScript>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GroundTransform = groundCheck.transform;
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
        moveblock = Input.GetAxisRaw("Moveblock");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < currentHorisontalInput && moveblock != 1)
        {

            currentHorisontalInput -= Mathf.Min(acceleration/accelerationDemodifire, currentHorisontalInput - horizontalInput);
        } else if (horizontalInput > currentHorisontalInput && moveblock != 1)
        {
            currentHorisontalInput += Mathf.Min(acceleration/accelerationDemodifire, horizontalInput - currentHorisontalInput);
        }
            isGrounded = Physics2D.OverlapCircle(GroundTransform.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (horizontalInput == -1)
        {
            RightSight = false;
        }
        else if (horizontalInput == 1)
        {
            RightSight = true;
        }

        if ((Input.GetAxisRaw("Fire1")) == 1)
        {
            FireProjectile();
        }
        UpDownSightInput = Input.GetAxisRaw("Vertical");
        LeftRightSightInput = Input.GetAxisRaw("Horizontal");

        switch (UpDownSightInput, LeftRightSightInput)
        {
            default:
                if(RightSight)
                {
                    sightDirection = 180f;
                }
                else if(!RightSight)
                {
                    sightDirection = 0f;
                }         
                break;
            case ( 0, 1):
                sightDirection = 180f;
                break;
            case (0, -1):
                sightDirection = 0f;
                break;
            case (-1, 0):
                sightDirection = 90f;
                break;
            case (1, 0):
                sightDirection = 270f;
                break;
            case (1, 1):
                sightDirection = 225f;
                break;
            case (-1, -1):
                sightDirection = 45f;
                break;
            case (1, -1):
                sightDirection = 315f;
                break;
            case (-1, 1):
                sightDirection = 135f;
                break;
        }
        Debug.Log(RightSight);
    }
    private void FireProjectile()
    {
        Instantiate(Projectile, transform.position, transform.rotation * Quaternion.Euler(0,0,sightDirection)).GetComponent<ProjectileScript>().SetStartConditions(1000,10,ProjectileSprite);
        
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(currentHorisontalInput * moveSpeed, rb.linearVelocity.y);
    }
}