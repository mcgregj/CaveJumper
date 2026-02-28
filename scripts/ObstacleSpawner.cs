using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    public GameObject obstaclePrefab;
    public Camera cam;

    public int maxObstacles = 1;
    public float borderMargin = 1.5f;

    private int currentObstacles = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void OnEnable()
{
    ScoreManager.OnScoreChanged += RespawnObstacles;
}

void OnDisable()
{
    ScoreManager.OnScoreChanged -= RespawnObstacles;
}
void RespawnObstacles()
{
    GameObject[] obstacles =
        GameObject.FindGameObjectsWithTag("Obstacle");

    foreach (GameObject obj in obstacles)
    {
        Destroy(obj);
        currentObstacles--;
    }

    SpawnIfNeeded();
}

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        SpawnObstacle();
    }

    public void ObstacleHit()
    {
        currentObstacles--;
        SpawnIfNeeded();
    }

    void SpawnIfNeeded()
    {
        if (currentObstacles < maxObstacles)
            SpawnObstacle();
    }

   void SpawnObstacle()
{
    float height = 2f * cam.orthographicSize;
    float width = height * cam.aspect;

    float left = cam.transform.position.x - width / 2 + borderMargin;
    float right = cam.transform.position.x + width / 2 - borderMargin;

    float randomX = Random.Range(left, right);

    // Top quarter spawn area
    float top = cam.transform.position.y + height / 2 - borderMargin;
    float upperQuarterStart = cam.transform.position.y + height * 0.25f;

    float randomY = Random.Range(upperQuarterStart, top);

    Instantiate(
        obstaclePrefab,
        new Vector2(randomX, randomY),
        Quaternion.identity
    );

    currentObstacles++;
}
}
