using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    private Button btn;
    public event Action <ButtonSelectManager> OnSelect;

    private void Awake()
    {
        highlight.SetActive(false);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => OnSelect?.Invoke(this));
    }
    public void SetHighlight(bool active)
    {
        highlight.SetActive (active);

    }

 
}