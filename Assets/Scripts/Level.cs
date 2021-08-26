//Egemen Engin
//https://github.com/egemenengin

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    public State state;
   
    private static Spawner spawner;
    GameObject JumpTxt;
    public static int numberOfPipeSpawn;

    public enum Difficulty
    {
        Easy ,
        Medium,
        Hard,
        Impossible
    }
    public enum State
    {
        waitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        numberOfPipeSpawn = 0;
        state = State.waitingToStart;
       
        
    }
    private void Start()
    {
        spawner = new Spawner();
        
        setDifficulty(Difficulty.Easy);
        JumpTxt = GameObject.Find("JUMP!");
        spawner.spawnGround();
       
        Bird.GetInstance().deathEvent += Bird_onDied;
        Bird.GetInstance().onStartedPlaying += Bird_startedToPlaying;
    }
    private void Update()
    {
        if (state == State.Playing)
        {
            spawner.handleGround();
            if (spawner.pipeSpawn())
            {
                numberOfPipeSpawn++;
            }
            spawner.pipeMovement();
        }

        setDifficulty(getDifficulty());

    }
    private void Bird_startedToPlaying(object sender, EventArgs e)
    {
        state = State.Playing;

        Destroy(JumpTxt.gameObject);
    }

    private void Bird_onDied(object sender, EventArgs e)
    {
        // FindObjectOfType<SceneLoader>().loadGameOver();
        state = State.Dead;
       
    }


    private Difficulty getDifficulty()
    {
      
        if (numberOfPipeSpawn >= 10) return Difficulty.Medium;
        if (numberOfPipeSpawn >= 20) return Difficulty.Hard;
        if (numberOfPipeSpawn >= 30) return Difficulty.Impossible;
        return Difficulty.Easy;


    }
    private void setDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                spawner.setGapSize(40f);
                break;
            case Difficulty.Medium:
                spawner.setGapSize(30f);
                break;

            case Difficulty.Hard:
                spawner.setGapSize(25f);
                break;

            case Difficulty.Impossible:
                spawner.setGapSize(20f);
                break;

        }


    }




}
