using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip Jumping;
    public AudioClip Crash;

    private AudioSource audio;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(Jumping);
        }
        else
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
    }

    public void JumpingSound()
    {
        audio.PlayOneShot(Jumping);
    }

    public void CrashSound()
    {
        audio.PlayOneShot(Crash);
    }

}
