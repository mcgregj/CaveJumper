using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    private bool isKnockedBack = false;
    private float knockbackTimer = 1f;
    public float knockbackDuration = 0.25f;
    public float fastFallAcceleration = 40f;
    public float jumpForce = 12f;
    public float maxFallSpeed = -25f;
    public float fastFallSpeed = -20f;
    private bool isSlipping = false;
    [SerializeField] private Animator _animator;
    public int maxJumps = 2;

    private int jumpsRemaining;
    private bool isFastFalling = false;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpPressed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        // Read input instantly
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && rb.linearVelocity.y < 0)
        {
            isFastFalling = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isFastFalling = false;
        }
    }
 
    void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("JumpRefill"))
    {
        isSlipping = false;
    }
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JumpRefill"))
        {
            jumpsRemaining = maxJumps;
            isSlipping = true;
        }
            if (other.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
            
        }
    }
    public void ApplyKnockback(Vector2 force)
{
    rb.linearVelocity = Vector2.zero;
    rb.AddForce(force, ForceMode2D.Impulse);

    isKnockedBack = true;
    knockbackTimer = knockbackDuration;
}
   void FixedUpdate()
{
    if (isKnockedBack)
{
    knockbackTimer -= Time.fixedDeltaTime;

    if (knockbackTimer <= 0f)
        isKnockedBack = false;

    return; // skip normal movement
}
    Vector2 vel = rb.linearVelocity;

    // Horizontal movement
    vel.x = moveInput * moveSpeed;

    // Fast fall (instant)
    if (isFastFalling)
    {
        vel.y = fastFallSpeed;
    }
    else
    {
        // Accelerated fall while holding S
        if (Input.GetKey(KeyCode.S) && vel.y < 0)
        {
            vel += Vector2.down * fastFallAcceleration * Time.fixedDeltaTime;
        }

        // Clamp maximum fall speed
        if (vel.y < maxFallSpeed)
        {
            vel.y = maxFallSpeed;
        }
    }

    // Jump
    if (jumpPressed)
    {
        vel.y = jumpForce;
        jumpPressed = false;
        jumpsRemaining--;
    }

    // Apply velocity
    rb.linearVelocity = vel;

    // Animations
    _animator.SetBool("isRunning", moveInput != 0);
    _animator.SetBool("isJumping", vel.y > 0);
    _animator.SetBool("isSlipping", isSlipping);
}
}