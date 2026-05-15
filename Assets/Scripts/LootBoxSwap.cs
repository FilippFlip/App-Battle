using System.CodeDom.Compiler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxSwap : MonoBehaviour
{
    public LootBoxData lootBoxData;//empty//
    public Image lootBoxIcon;
  
    public GameObject casesPanel;
    public GameObject lootBoxMenu;
    public TMP_Text lootBoxName;
    public Transform profileSpace;
    public AppSlot slotPrefab;
    private float totalweight;
    private int slotMachineNumber;
    public GameObject boxInfo;
    public GameObject slotPanel;
    public GameObject slotMachinePrefab;

    public GameObject blocker;
    public GameObject arrowBack;
    private void Start()
    {
        blocker.SetActive(false);
    }
    public void SetLootBoxData(LootBoxData data)
    {
        casesPanel.SetActive(false);    
        lootBoxMenu.SetActive(true);
        lootBoxData = data;
        
        lootBoxIcon.sprite = lootBoxData.icon;
        lootBoxName.text = lootBoxData.name;
        totalweight = 0;
        for (int i=profileSpace.childCount-1;i>=0;i--)
        {
            Destroy(profileSpace.GetChild(i).gameObject);
        }
        foreach (ItemChance item in lootBoxData.lootBoxData)
        {
            totalweight += item.weight;

        }
        foreach (ItemChance item in lootBoxData.lootBoxData)
        {
            GenerateIcon(item , profileSpace);
        }
    }
    public void GenerateIcon(ItemChance chance , Transform parent)
    {
        AppSlot slot = Instantiate(slotPrefab, parent);
        slot.icon.sprite = chance.item.icon;
        slot.price.text = chance.item.price.ToString();
        slot.chance.text = (chance.weight / totalweight * 100).ToString()+"%";

    }
    public void SelectMachinesNumber (int amount)
    {
        slotMachineNumber = amount;

    }
    public void GenerateLootBoxes()
    {
        blocker.SetActive(true);
        arrowBack.SetActive(false);

        boxInfo.SetActive(false);
        slotPanel.SetActive(true);
        for (int i = 0; i < slotMachineNumber; i++)
        {
            Instantiate(slotMachinePrefab, slotPanel.transform);
            
        }
    }

}
