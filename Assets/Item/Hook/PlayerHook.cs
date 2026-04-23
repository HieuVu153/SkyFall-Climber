using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public LineRenderer line;
    public float hookDistance = 10f;
    public float pullForce = 20f;
    public LayerMask hookLayer;

    public int hookCount = 0; // 🔥 số lần dùng

    private Rigidbody2D rb;
    private bool isHooking = false;
    private Vector2 hookPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line.enabled = false;

        hookCount = 0; // 🔒 mặc định không có lượt dùng
    }

    public void FireHook()
    {
        // 🔒 HẾT LƯỢT → KHÔNG CHO DÙNG
        if (hookCount <= 0)
        {
            Debug.Log("Hết Hook rồi!");
            return;
        }

        if (isHooking) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, hookDistance, hookLayer);

        if (hit.collider != null)
        {
            hookPoint = hit.point;
            isHooking = true;

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookPoint);

            hookCount--; // 🔥 dùng 1 lần → trừ 1
            Debug.Log("Còn lại: " + hookCount);
        }
        else
        {
            Debug.Log("Không có điểm móc!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireHook();
        }

        if (!isHooking) return;

        // vẽ dây
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hookPoint);

        // kéo player
        Vector2 dir = (hookPoint - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * pullForce; // ✅ FIX đúng API

        // tới nơi thì dừng
        if (Vector2.Distance(transform.position, hookPoint) < 4f)
        {
            StopHook();
        }
    }

    void StopHook()
    {
        isHooking = false;
        line.enabled = false;

        rb.linearVelocity = Vector2.zero; // ✅ FIX đúng API
    }
}