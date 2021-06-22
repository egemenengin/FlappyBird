using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    ScoreWindow scoreWindow;
    public void Start()
    {
        scoreWindow = FindObjectOfType<ScoreWindow>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        scoreWindow.increaseScore();
    
    }
    public static int getHighScore() {
        return PlayerPrefs.GetInt("highScore");
    }
    public static bool trySetNewHighscore(int score)
    {
        if (score > getHighScore())
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
          return  false;
        }
    }
        
}
