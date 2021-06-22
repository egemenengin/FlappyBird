using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreWindow : MonoBehaviour
{
    TextMeshProUGUI score;
    TextMeshProUGUI highscore;
    [SerializeField] AudioClip scoreSound;
    [SerializeField] [Range(0, 1)] float scoreVolume = 1f;
   
    Level level;
    int curScore;
    private void Awake()
    {
        score = transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        highscore = transform.Find("highScoreTxt").GetComponent<TextMeshProUGUI>();
      
    }
    public void Start()
    {
        curScore = 0;
        level = FindObjectOfType<Level>();
        highscore.text = "HighScore: " +ScoreCounter.getHighScore().ToString();
        Bird.GetInstance().deathEvent += birdDied;
        GameObject.Find("ScoreTxt").SetActive(true);
        GameObject.Find("highScoreTxt").SetActive(true);
    }

    private void birdDied(object sender, EventArgs e)
    {
        GameObject.Find("ScoreTxt").SetActive(false);
        GameObject.Find("highScoreTxt").SetActive(false);
    }

    public void Update()
    {
        score.text = curScore.ToString();
    }
    public void increaseScore()
    {
        curScore++;
        AudioSource.PlayClipAtPoint(scoreSound, Camera.main.transform.position, scoreVolume);
    }
    public int getScore()
    {
        return curScore;
    }
}
