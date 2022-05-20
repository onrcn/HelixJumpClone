using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;

    void Update()
    {
        scoreText.text = "Score: " + GameManager.singleton.currentScore;    
        highScoreText.text = "High Score: " + GameManager.singleton.highScore;
    }
}
