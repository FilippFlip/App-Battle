using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource source;
    public AudioSource music;
    public AudioClip[] musics;

    private void Start()
    {
        RandomizedMusic();
    }
    private void Update()
    {
        if (music.isPlaying==false)
        {
            RandomizedMusic();
        }
    }
    public void ButtonClick()
    {
        source.clip = clickSound;
        source.Play();
    }
    public void RandomizedMusic()
    {
        int random= Random.Range(0, musics.Length);
        AudioClip audioClip = musics[random];
        music.clip = audioClip;
        music.Play();
    }

}
