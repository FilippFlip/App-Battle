using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Blocker : MonoBehaviour, IPointerClickHandler, IScrollHandler
{
    public GameObject warningPanel;
    public ScrollRect content;
    public async void OnPointerClick(PointerEventData eventData)
    {
        if (warningPanel.activeSelf)
        {
            return ;
        }
        warningPanel.SetActive(true);
        await Awaitable.WaitForSecondsAsync(1.5f);
        warningPanel.SetActive(false);
    }

    public void OnScroll(PointerEventData eventData)
    {
        content.OnScroll(eventData);
    }
}
