//Egemen Engin
//https://github.com/egemenengin

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Rigidbody2D rg2D;
    Animator BirdAnimator;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float rightForce = 0.5f;
    public static Bird instance;
    private State state;
    public event EventHandler deathEvent;
    public event EventHandler onStartedPlaying;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] [Range(0, 1)] float jumpVolume = 1f;
    public enum State
    {
        waitingToStart,
        Playing,
        Dead
    }
    public static Bird GetInstance()
    {
        return instance;
    }
    
    public void Awake()
    {
        instance = this;
        rg2D = GetComponent<Rigidbody2D>();
        BirdAnimator = GetComponent<Animator>();
        rg2D.bodyType = RigidbodyType2D.Static;
        state = State.waitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.waitingToStart:
               
                
                //if (Input.GetKeyDown(KeyCode.Space))
                if ((Input.GetButtonDown("Jump")) && transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 12f)
                {
                    state = State.Playing;
                    rg2D.bodyType = RigidbodyType2D.Dynamic;
                    jump();
                    if (onStartedPlaying != null) onStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                //if (Input.GetKeyDown(KeyCode.Space))
                if ((Input.GetButtonDown("Jump")) && transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 12f)
                {
                    jump();
                }
                transform.eulerAngles = new Vector3(0, 0, rg2D.velocity.y*0.5f);
                break;
            case State.Dead:
                break;

        }
        
         
    }
    public void jump()
    {
        rg2D.velocity = Vector2.up * jumpForce;

        AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position,jumpVolume );



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        state = State.Dead;
        rg2D.bodyType = RigidbodyType2D.Static;
        BirdAnimator.enabled = false;
       
        //FindObjectOfType<SceneLoader>().loadGameOver();
        if (deathEvent != null) deathEvent(this, EventArgs.Empty);
       
    }
    
}
