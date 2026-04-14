using UnityEngine;

public class LandingSound : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip landingSound;

    private bool wasGrounded;

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Nếu trước đó đang ở trên không và bây giờ chạm đất → phát sound
        if (!wasGrounded && isGrounded)
        {
            PlayLandingSound();
        }

        wasGrounded = isGrounded;
    }

    void PlayLandingSound()
    {
        if (audioSource != null && landingSound != null)
        {
            audioSource.PlayOneShot(landingSound);
        }
    }
}