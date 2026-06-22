using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SlotMachineContainer : MonoBehaviour
{
    
    private CanvasGroup group;
    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        SetActive(false);
        
    }
    public void SetActive(bool active)
    {
        if (active)
        {
            
            group.alpha = 1.0f;
            OpenAnimation();
        }
        else 
        {
            group.alpha = 0.0f;
        }

    }
    private async void OpenAnimation()
    {
        float timer=0;
        var rect=GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
        while (timer<=1)
        {
            timer += Time.deltaTime;

            await Awaitable.NextFrameAsync();
            rect.localScale = Vector3.one * timer;
        }
        rect.localScale = Vector3.one;
    }
}
