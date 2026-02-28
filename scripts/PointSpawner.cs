using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject pointPrefab;
    public Camera cam;

    public int maxPoints = 1;
    public float borderMargin = 1.5f; // ⭐ distance from camera border

    private int currentPoints = 0;

    void Start()
    {
        SpawnPoint();
    }

    public void PointCollected()
    {
        currentPoints--;

        if (currentPoints < maxPoints)
        {
            SpawnPoint();
        }
    }

    void SpawnPoint()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        float randomX = Random.Range(
            cam.transform.position.x - width / 2 + borderMargin,
            cam.transform.position.x + width / 2 - borderMargin);

        float randomY = Random.Range(
            cam.transform.position.y - height / 2 + borderMargin,
            cam.transform.position.y + height / 2 - borderMargin);

        Vector2 spawnPos = new Vector2(randomX, randomY);

        Instantiate(pointPrefab, spawnPos, Quaternion.identity);

        currentPoints++;
    }
}