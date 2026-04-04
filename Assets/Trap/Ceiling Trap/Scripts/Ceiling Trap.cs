using UnityEngine;

public class CeilingTrap : MonoBehaviour
{
    public float detectionRadius = 3f;
    public LayerMask playerLayer;

    private bool hasDealtDamage = false;
    private bool isTriggered = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (isTriggered) return;

        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (player != null)
        {
            isTriggered = true;
            anim.SetTrigger("Attack"); // phải có trong Animator
        }
    }

    //// GỌI TỪ ANIMATION EVENT (lúc trap dập xuống)
    //public void DealDamage()
    //{
    //    if (hasDealtDamage) return;

    //    Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

    //    if (player != null)
    //    {
    //        float dirX = player.transform.position.x > transform.position.x ? 1f : -1f;
    //        Vector2 direction = new Vector2(dirX, 0f);

    //        player.GetComponent<PlayerController>().Knockback(direction);

    //        hasDealtDamage = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(collision.GetComponent<PlayerController>().DelayKnock(Vector2.right));
        }
    }

    // GỌI Ở CUỐI ANIMATION HỒI CHIÊU
    public void ResetTrap()
    {
        hasDealtDamage = false;
        isTriggered = false;
    }

    // VẼ VÙNG PHÁT HIỆN
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}