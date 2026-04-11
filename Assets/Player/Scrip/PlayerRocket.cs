<<<<<<< HEAD
﻿
=======
using UnityEngine;
using System.Collections;

public class PlayerRocket : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    public GameObject rocketVisual; // kéo Rocket vào đây

    private bool isRocketActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rocketVisual.SetActive(false); // ẩn lúc đầu
    }

    public void ActivateRocket(float force, float duration)
    {
        if (!isRocketActive)
        {
            StartCoroutine(RocketCoroutine(force, duration));
        }
    }

    IEnumerator RocketCoroutine(float force, float duration)
    {
        isRocketActive = true;

        // Hiện rocket
        rocketVisual.SetActive(true);

        // Tắt va chạm
        col.enabled = false;

        // Reset vận tốc
        rb.linearVelocity = Vector2.zero;

        // Bay lên
        rb.gravityScale = 0;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        // Ẩn lại
        rocketVisual.SetActive(false);

        // Trả lại bình thường
        rb.gravityScale = 8;
        col.enabled = true;

        isRocketActive = false;
    }
}
>>>>>>> parent of 83668b19a (Merge branch 'main' of https://github.com/HieuVu153/SkyFall-Climber)
