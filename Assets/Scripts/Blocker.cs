using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocker : MonoBehaviour, IPointerClickHandler
{
    public GameObject warningPanel;

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
}
