using UnityEngine;
using System.Collections;

public class PlayerRocket : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    public GameObject rocketVisual;

    private bool isRocketActive = false;

    private float defaultGravity;
    private Coroutine rocketCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        defaultGravity = rb.gravityScale;

        if (rocketVisual != null)
            rocketVisual.SetActive(false);
    }

    public void ActivateRocket(float force, float duration)
    {
        if (isRocketActive) return;

        if (rocketCoroutine != null)
            StopCoroutine(rocketCoroutine);

        rocketCoroutine = StartCoroutine(RocketCoroutine(force, duration));
    }

    IEnumerator RocketCoroutine(float force, float duration)
    {
        isRocketActive = true;

        // bật visual
        if (rocketVisual != null)
            rocketVisual.SetActive(true);

        // tắt va chạm
        if (col != null)
            col.enabled = false;

        // reset lực
        rb.linearVelocity = Vector2.zero;

        // tắt trọng lực
        rb.gravityScale = 0;

        float timer = 0;

        // 🚀 bay liên tục mượt hơn
        while (timer < duration)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
            timer += Time.deltaTime;
            yield return null;
        }

        // tắt visual
        if (rocketVisual != null)
            rocketVisual.SetActive(false);

        // trả lại trạng thái ban đầu
        rb.gravityScale = defaultGravity;

        if (col != null)
            col.enabled = true;

        isRocketActive = false;
    }
}