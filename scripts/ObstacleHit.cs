using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    public float knockbackForce = 8f;
    public int scorePenalty = 1;
    public float knockbackCooldown = 1f;

    private float knockbackTimer = 0f;

    void Update()
    {
        if (knockbackTimer > 0)
            knockbackTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") && knockbackTimer <= 0f)
        {
            knockbackTimer = knockbackCooldown;

            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                float dir = Mathf.Sign(
                    other.transform.position.x - transform.position.x);

                Vector2 knockback =
                    new Vector2(dir, 0.6f).normalized;

                playerRb.linearVelocity = Vector2.zero;
                playerRb.AddForce(knockback * knockbackForce,
                                  ForceMode2D.Impulse);
            }

            ScoreManager.Instance.RemovePoints(scorePenalty);
        }
    }
}