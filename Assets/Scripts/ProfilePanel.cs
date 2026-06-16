using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    public PlayerProfile profile;
    public AppSlot slotPrefab;
    public Transform itemsParent;
    public TMP_Text netWorth;
    public TMP_Text crystals;
    public TMP_Text buttonText;

    public float Crystals
    {
        set
        {
            profile.crystals = value;
            crystals.text = profile.crystals.ToString();
        }
        get
        {
            return profile.crystals;
        }
    }

    public float NetWorth
    {
        get
        {
            float netWorth = 0;
            foreach (AppData app in profile.wonApps)
            {
                netWorth += app.price;
            }
            netWorth += profile.crystals;
            return netWorth;
        }
    }

    private void Awake()
    {
        netWorth.text = profile.crystals.ToString();
        crystals.text = profile.crystals.ToString();
    }
    private void OnEnable()
    {
        profile.OnItemAdded += AddItem;
        profile.OnItemRemoved += RemoveItem;
    }

    private void OnDisable()
    {
        profile.OnItemAdded -= AddItem;
        profile.OnItemRemoved -= RemoveItem;
    }
    public void Show()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }
        foreach (AppData app in profile.wonApps)
        {
            AppSlot slot = Instantiate(slotPrefab, itemsParent);
            slot.icon.sprite = app.icon;
            slot.appData = app;
            slot.price.text = app.price.ToString();
        }
        netWorth.text = NetWorth.ToString();
        UpdateSellButtonText(); 
    }


    private void UpdateSellButtonText()
    {
        float totalSellValue = 0;
        foreach (AppData app in profile.wonApps)
        {
            totalSellValue += app.price;
        }

        if (totalSellValue > 0)
            buttonText.text = $"Sell All ({totalSellValue})";
        else
            buttonText.text = "Sell All";
    }

    public void SellAll()
    {
        foreach (AppData app in profile.wonApps)
        {
            Crystals += app.price;
        }

        for (int i = profile.wonApps.Count - 1; i >= 0; i--)
        {
            profile.wonApps.RemoveAt(i);
        }
        Show();
    }
    private void AddItem(AppData item)
    {
        
    }
    private void RemoveItem(AppData item)
    {
        Crystals += item.price;
    }
}