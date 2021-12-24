using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    
    public static AudioClip playCardSound, stealNotificationSound, passSound, red10sound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playCardSound = Resources.Load<AudioClip>("cardsound");
        stealNotificationSound = Resources.Load<AudioClip>("StealNotification");
        passSound = Resources.Load<AudioClip>("passSound");
        red10sound = Resources.Load<AudioClip>("red10sound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "NormalPlay":
                audioSrc.PlayOneShot(playCardSound);
                break;
            case "StealNotification":
                audioSrc.PlayOneShot(stealNotificationSound);
                break;
            case "PassSound":
                audioSrc.PlayOneShot(passSound);
                break;
            case "Red10sound":
                audioSrc.PlayOneShot(red10sound);
                break;
        }
    }
}
