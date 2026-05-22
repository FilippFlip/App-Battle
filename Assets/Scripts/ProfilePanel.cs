using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    public PlayerProfile profile;
    public AppSlot slotPrefab;
    public Transform itemsParent;
    public TMP_Text netWorth;
    public TMP_Text crystals;

    private void Awake()
    {
        netWorth.text = profile.crystals.ToString();
        crystals.text = profile.crystals.ToString();
        
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

        netWorth.text = profile.crystals.ToString();
        crystals.text = profile.crystals.ToString();
    }
    public void SellAll()
    {
        foreach(AppData app in profile.wonApps)
        {
            profile.crystals += app.price;

        }
        
        for(int i = profile.wonApps.Count - 1; i >= 0; i--)
        {
            profile.wonApps.RemoveAt(i);
        }
        Show();
    }
}
