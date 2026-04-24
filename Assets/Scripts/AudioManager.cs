using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource source;
    public void ButtonClick()
    {
        source.clip = clickSound;
        source.Play();
    }
}
