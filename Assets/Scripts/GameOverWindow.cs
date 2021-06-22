using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverWindow : MonoBehaviour
{
    TextMeshProUGUI finalScoreTxt;
    TextMeshProUGUI highscore;
    
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] [Range(0, 1)] float gameOverVolume = 1f;
    private void Start()
    {
        finalScoreTxt = transform.Find("FinalScoreTxt").GetComponent<TextMeshProUGUI>();
        highscore = transform.Find("HighScoreTxt").GetComponent<TextMeshProUGUI>();
        Bird.GetInstance().deathEvent += birdDied;
        Hide();
    }

    private void birdDied(object sender, EventArgs e)
    {
        
        finalScoreTxt.text = FindObjectOfType<ScoreWindow>().getScore().ToString();
        ScoreCounter.trySetNewHighscore(FindObjectOfType<ScoreWindow>().getScore());
        
        highscore.text = ScoreCounter.getHighScore().ToString();
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position, gameOverVolume);
        Show();
        
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
