using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class UpgradeManager : MonoBehaviour
{
    public AllAppData appData;
    public Transform rightContent;
    public Transform leftContent;
    public AppSlot rightAppSlot;
    public GameObject leftInfo;
    public GameObject rightInfo;
    public Image leftIcon;
    public Image rightIcon;
    public TMP_Text leftPrice;
    public TMP_Text rightPrice;
    public PlayerProfile profile;
    public UpgradeChance chance1;

    private AppData lSlot;
    private AppData rSlot;
    
    private void Update()
    {
        if (rSlot!= null&&lSlot!=null)
        {
            float chance =(float) lSlot.price /(float) rSlot.price ;
            chance1.chance = chance;
        }
        else
        {
            chance1.chance = 0;
        }
    }
    private void OnEnable()
    {
        leftInfo.SetActive(false);
        rightInfo.SetActive(false);
        foreach (var entry in appData.apps.OrderBy(entry => entry.app.price))
        {
            if (entry.visibleInUpgrade == false)
            {
                continue;
            }
            var obj = Instantiate(rightAppSlot, rightContent);
            obj.icon.sprite = entry.app.icon;
            obj.price.text = entry.app.price.ToString();
            obj.appData = entry.app;
            obj.GetComponent<Button>().onClick.AddListener(() => FillRightInfoSlot(obj));
        }
        foreach (var app in profile.wonApps)
        {
            var obj = Instantiate(rightAppSlot, leftContent);
            obj.icon.sprite = app.icon;
            obj.price.text = app.price.ToString();
            obj.appData = app;
            obj.GetComponent<Button>().onClick.AddListener(() => FillLeftInfoSlot(obj));
        }
    }
    private void OnDisable()
    {
        rSlot = null;
        lSlot = null;

    }
    private void FillRightInfoSlot(AppSlot slot)
    {
        rightInfo.SetActive(true);
        rightIcon.sprite = slot.appData.icon;
        rightPrice.text = slot.appData.price.ToString();
        rSlot = slot.appData;
    }
    private void FillLeftInfoSlot(AppSlot slot) 
    {
        leftInfo.SetActive(true);
        leftIcon.sprite = slot.appData.icon;
        leftPrice.text = slot.appData.price.ToString();
        lSlot = slot.appData;
    }

}
