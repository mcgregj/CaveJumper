using UnityEngine;
using UnityEngine.UI;
using System; // ⭐ REQUIRED for Action

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // ⭐ ADD THIS LINE
    public static event Action OnScoreChanged;

    public Text scoreText;
    private int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint(int amount)
    {
        score += amount;
        UpdateScoreUI();

        OnScoreChanged?.Invoke();
    }

    public void RemovePoints(int amount)
    {
        score -= amount;

        if (score < 0)
            score -= score; // prevent negative scores

        UpdateScoreUI();

        OnScoreChanged?.Invoke(); // ⭐ also trigger here
    }

    public void LoseHalfScore()
    {
        score -= 2;
        UpdateScoreUI();

        OnScoreChanged?.Invoke(); // ⭐ keep obstacles synced
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}