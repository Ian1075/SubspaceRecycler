using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool canMove = false;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private bool dashIsEnabled = true;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private bool isDashing;
    private float dashCooldownTimer;

    private Rigidbody2D rb;
    private Vector2 moveInput;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCooldownTimer = 0f;
        Invoke("EnableMovement", 1f);
    }
    void Update()
    {
        if (!canMove) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (dashIsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
            {
                StartCoroutine(Dash());
            }
            if (dashCooldownTimer > 0)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
        }

    }

    void FixedUpdate()
    {
        if (!canMove) return;
        if (isDashing) return;
        rb.velocity = moveInput * moveSpeed;
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        Vector2 dashDirection = moveInput.magnitude > 0 ? moveInput : Vector2.up;

        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero;
    }
    
    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
        // 當移動被禁止時，最好也將速度歸零，避免角色滑動
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}