using System.Runtime.CompilerServices;
using UnityEngine;

public class NumberSelectManager : MonoBehaviour
{
    public ButtonSelectManager[] buttons;
    void Start()
    {
        foreach (var btn in buttons)
        {
            btn.OnSelect += Selected;
        } 
    }
    private void Selected(ButtonSelectManager b)
    {
        foreach (var btn in buttons)
        {
            btn.SetHighlight(btn == b);
        }
    }
   
}
