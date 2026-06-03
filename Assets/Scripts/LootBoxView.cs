using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxView : MonoBehaviour
{
    public Image lootBoxIcon;
    public TMP_Text lootBoxName;
    public Transform profileSpace;
    public AppSlot slotPrefab;
    public TMP_Text priceText;
    private LootBoxData _data;
    public void Show(LootBoxData data)
    {
        _data = data;
        lootBoxIcon.sprite = data.icon;
        lootBoxName.text = data.name;
        priceText.text=data.price.ToString();
        ClearGrid();

        float totalWeight = 0;
        foreach (ItemChance item in data.lootBoxData)
        {
            totalWeight += item.weight;
        }

        foreach (ItemChance item in data.lootBoxData)
        {
            GenerateIcon(item, totalWeight);
        }
    }

    private void ClearGrid()
    {
        for (int i = profileSpace.childCount - 1; i >= 0; i--)
        {
            Destroy(profileSpace.GetChild(i).gameObject);
        }
    }

    private void GenerateIcon(ItemChance chance, float totalWeight)
    {
        AppSlot slot = Instantiate(slotPrefab, profileSpace);
        slot.icon.sprite = chance.item.icon;
        slot.price.text = chance.item.price.ToString();
        slot.chance.text = (chance.weight / totalWeight * 100).ToString() + "%";
    }
    public void ChangeLootBoxPrice(int multi)
    {
        priceText.text=(_data.price*multi).ToString();
    }
}