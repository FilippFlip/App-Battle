using UnityEngine;

public class RollSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Vector2 pitchRange;
    private SlotMachine machine;

    private async void OnEnable()
    {
        machine=GetComponentInChildren<SlotMachine>();
        await Awaitable.WaitForSecondsAsync(1);
        machine.OnAppCreated += Tick;

    }

    private void OnDisable()
    {
        machine.OnAppCreated -= Tick;

    }
    private void Tick()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
