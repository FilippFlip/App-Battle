using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image shadow;
    
    public Button button;
    
    public Animation shadowAnimation;
    void Start()
    {
        button.onClick.AddListener(ButtonClick);
        
    }

    private void OnEnable()
    {
        shadowAnimation.Play("Shadow Off");
        shadowAnimation["Shadow Off"].normalizedTime = 1;

    }
    private void ButtonClick()
    {
        print("click");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        float normallTime = shadowAnimation["Shadow On"].normalizedTime;
        if (normallTime > 0)
        {
            shadowAnimation["Shadow Off"].normalizedTime = 1 - normallTime;
        }

        shadowAnimation.Play("Shadow Off");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float normallTime = shadowAnimation["Shadow Off"].normalizedTime;
        if (normallTime > 0)
        {
            shadowAnimation["Shadow On"].normalizedTime = 1 - normallTime;
        }
        shadowAnimation.Play("Shadow On");
    }
    }
