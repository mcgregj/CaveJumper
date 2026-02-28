using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PointSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<PointSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("point"))
        {
            spawner.PointCollected();

            ScoreManager.Instance.AddPoint(1);

            Destroy(other.gameObject);
        }
    }
}