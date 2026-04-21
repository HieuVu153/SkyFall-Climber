using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float maxChargeTime = 1.5f;

    [Header("Fly Mode")]
    public float flySpeed = 8f;
    private bool isFlyMode = false;
    private Collider2D col;
    private float originalGravity; // lưu gravity gốc

    private float chargeTime;
    private bool isCharging = false;
    private bool isGrounded;

    public CoinManager coinManager;

    private Rigidbody2D rb;
    private Animator anim;
    public float knockbackForce = 10f;
    Vector2 dirForce = Vector2.zero;
    bool addForce;

    public int score = 0;

    private float moveInput;
    private int facingDirection = 1; // 1 = phải, -1 = trái

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        originalGravity = rb.gravityScale; // lưu gravity ban đầu
    }

    void Update()
    {
        // ===== Toggle Fly Mode =====
        if (Input.GetKeyDown(KeyCode.P))
        {
            isFlyMode = !isFlyMode;

            if (isFlyMode)
            {
                rb.gravityScale = 0;
                col.isTrigger = true;
            }
            else
            {
                rb.gravityScale = originalGravity; // trả lại gravity gốc
                col.isTrigger = false;
                rb.linearVelocity = Vector2.zero; // reset lực bay còn sót
            }
        }

        // ===== Điều khiển bay =====
        if (isFlyMode)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            rb.linearVelocity = new Vector2(x * flySpeed, y * flySpeed);
            return;
        }

        // ===== CODE GỐC =====
        Move();
        Jump();

        anim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

        if (addForce) transform.Translate(dirForce * 20f * Time.deltaTime);
    }

    void Move()
    {
        if (!isGrounded && !isCharging) return;

        moveInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1;
            Flip(-1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1;
            Flip(1);
        }

        if (isCharging)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isCharging = true;
            chargeTime = 0;
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            float power = chargeTime / maxChargeTime;

            float jumpY = jumpForce * power;
            float jumpX = facingDirection * jumpForce * power;

            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(jumpX * 100, jumpY * 200));

            isCharging = false;
            isGrounded = false;
        }
    }

    void Flip(int direction)
    {
        facingDirection = direction;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

    public void Knockback(Vector2 direction)
    {
        // rb.linearVelocity = Vector2.zero;
        // rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    public IEnumerator DelayKnock(Vector2 direction)
    {
        dirForce = direction;
        addForce = true;
        yield return new WaitForSeconds(.4f);
        addForce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            if (coinManager != null)
            {
                coinManager.AddCoin(1);
            }
            else
            {
                Debug.LogError("Chưa gắn CoinManager!");
            }

            Destroy(collision.gameObject);
        }
    }
}