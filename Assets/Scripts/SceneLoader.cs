using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int curScene;
    [SerializeField] int timeToWait = 4;


    public void Start()
    {
        curScene = SceneManager.GetActiveScene().buildIndex;
        if (curScene == 3)
        {
            StartCoroutine(waitForTime());
        }
    }

    IEnumerator waitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("GameScene");
    }
    public void loadNextScene()
    {
        curScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curScene + 1);

    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void loadGameOver()
    {
        SceneManager.LoadScene("GameOver");

    }
    public void loadGameScene()
    {
        SceneManager.LoadScene("GameScene");



    }
    public void loadLoadingScene()
    {
        SceneManager.LoadScene("Loading");

    }
    public void loadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");

    }
    



}
