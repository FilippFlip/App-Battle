using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MMButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image shadow;
    public Image icon;
    public Button button;
    public Animation iconAnimation;
    public Animation shadowAnimation;
    void Start()
    {
        button.onClick.AddListener(ButtonClick);

    }
    private void OnEnable()
    {
        iconAnimation.Play("Button down");
        shadowAnimation.Play("Shadow Off");
        iconAnimation["Button down"].normalizedTime = 1;
        shadowAnimation["Shadow Off"].normalizedTime = 1;

    }

    private void ButtonClick()
    {
    

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        float normallTime = iconAnimation["Button up"].normalizedTime;
        if (normallTime > 0)
        {
            iconAnimation["Button down"].normalizedTime = 1 - normallTime;
            shadowAnimation["Shadow Off"].normalizedTime = 1 - normallTime;
        }

        iconAnimation.Play("Button down");
        shadowAnimation.Play("Shadow Off");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float normallTime = iconAnimation["Button down"].normalizedTime;
        if (normallTime > 0)
        {
            iconAnimation["Button up"].normalizedTime = 1 - normallTime;
            shadowAnimation["Shadow On"].normalizedTime = 1 - normallTime;
        }
        iconAnimation.Play("Button up");
        shadowAnimation.Play("Shadow On");
    }
    
}
