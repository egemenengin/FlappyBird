//Egemen Engin
//https://github.com/egemenengin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource soundFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;
 public void ClickSound()
    {
        soundFX.PlayOneShot(clickFX);
    }
 public void HoverSound()
    {
        soundFX.PlayOneShot(hoverFX);
    }
}
