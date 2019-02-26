using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public AudioClip music;
    public AudioClip explosion;
    public AudioClip attack;
    //public AudioClip collide;
    //public AudioClip death;
    //public AudioClip pause;

    private AudioSource musicSource;

    public enum SFX { Music, Explosion, Attack, Collide, Death, Pause};

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Init();
    }

    public void Init()
    {
        //musicSource = new AudioSource();
        //musicSource.clip = music;
        //musicSource.spatialBlend = 0;
        //musicSource.Play();
    }

    public void playClip(SFX sfx, Vector3 location)
    {
        if(sfx == SFX.Explosion)
        {
            AudioSource.PlayClipAtPoint(explosion, location);
        }else if(sfx == SFX.Collide)
        {
            AudioSource.PlayClipAtPoint(attack, location);
        }

    }

}
