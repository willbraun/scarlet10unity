using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    
    public static AudioClip playCardSound, stealNotificationSound, passSound, red10sound, resultSound, gameoverSound, startSound, exitSound;
    static AudioSource audioSrc;

    void Start()
    {
        playCardSound = Resources.Load<AudioClip>("cardsound");
        stealNotificationSound = Resources.Load<AudioClip>("StealNotification");
        passSound = Resources.Load<AudioClip>("passSound");
        red10sound = Resources.Load<AudioClip>("red10sound");
        resultSound = Resources.Load<AudioClip>("resultSound");
        gameoverSound = Resources.Load<AudioClip>("gameoverSound");
        startSound = Resources.Load<AudioClip>("startSound");
        exitSound = Resources.Load<AudioClip>("exitSound");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "NormalPlay":
                audioSrc.volume = 1.0f;
                audioSrc.PlayOneShot(playCardSound);
                break;
            case "StealNotification":
                audioSrc.volume = 0.7f;
                audioSrc.PlayOneShot(stealNotificationSound);
                break;
            case "PassSound":
                audioSrc.volume = 1.0f;
                audioSrc.PlayOneShot(passSound);
                break;
            case "Red10sound":
                audioSrc.volume = 0.2f;
                audioSrc.PlayOneShot(red10sound);
                break;
            case "ResultSound":
                audioSrc.volume = 0.2f;
                audioSrc.PlayOneShot(resultSound);
                break;
            case "GameOverSound":
                audioSrc.volume = 0.8f;
                audioSrc.PlayOneShot(gameoverSound);
                break; 
            case "StartSound":
                audioSrc.volume = 0.7f;
                audioSrc.PlayOneShot(startSound);
                break;
            case "ExitSound":
                audioSrc.volume = 1.0f;
                audioSrc.PlayOneShot(exitSound);
                break;   
        }
    }
}
