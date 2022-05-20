using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int highScore;
    public int currentScore;

    public int currentLevel = 0;

    public static GameManager singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {

    }

    public void NextLevel()
    {
        currentLevel++;
        FindObjectOfType<BallController>().ResetBallPosition();
        FindObjectOfType<HelixController>().LoadStage(currentLevel);
    }

    public void GameOver()
    {
        singleton.currentScore = 0;
        if (singleton.currentScore > singleton.highScore)
        {
            singleton.highScore = singleton.currentScore;
            PlayerPrefs.SetInt("HighScore", singleton.highScore);
        }
        FindObjectOfType<BallController>().ResetBallPosition();
        FindObjectOfType<HelixController>().LoadStage(currentLevel);

    }

    public void AddScore(int score)
    {
        currentScore += score;
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
