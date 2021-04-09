using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score;

    public int Score { get => score; set => score = value; }
    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;
    }

    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }


}
