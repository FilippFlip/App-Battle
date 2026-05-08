using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectManager : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject highlight;

    private void Awake()
    {
        if (highlight != null)
            highlight.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (highlight != null)
            highlight.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (highlight != null)
            highlight.SetActive(false);
    }
}