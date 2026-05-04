using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    public PlayerProfile profile;
    public AppSlot slotPrefab;
    public Transform itemsParent;
    public TMP_Text crystalsText;

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

        }

        crystalsText.text = profile.crystals.ToString();
    }
}
