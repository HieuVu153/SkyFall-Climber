using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float maxChargeTime = 1.5f;

    private float chargeTime;
    private bool isCharging = false;
    private bool isGrounded;

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
    }

    void Update()
    {
        Move();
        Jump();

        // cập nhật animation speed (Idle/Run)
        anim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

        if (addForce) transform.Translate(dirForce * 20f * Time.deltaTime);
    }

    void Move()
    {
        // Chỉ khóa khi đang bay, không khóa khi đang charge
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

        //  Nếu đang charge thì KHÔNG cho trượt (đứng yên nhưng vẫn xoay)
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
        //rb.linearVelocity = Vector2.zero; // reset lực cũ
        //rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
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
            GameManager.instance.AddScore(1);
            Destroy(collision.gameObject);
        }
    }

}