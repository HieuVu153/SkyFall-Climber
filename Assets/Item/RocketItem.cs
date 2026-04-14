using UnityEngine;

public class RocketItem : MonoBehaviour
{
    public float rocketForce = 30f;
    public float rocketDuration = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRocket rocket = other.GetComponent<PlayerRocket>();
            if (rocket != null)
            {
                rocket.ActivateRocket(rocketForce, rocketDuration);
            }

            gameObject.SetActive(false); // ❌ KHÔNG destroy nữa
        }
    }
}