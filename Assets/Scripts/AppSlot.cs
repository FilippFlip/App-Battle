using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppSlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text price;
    public TMP_Text chance;
    public AppData appData;
    public PlayerProfile profile;
    public void SellSlot()
    {
        profile.RemoveItem(appData);
    }
}
