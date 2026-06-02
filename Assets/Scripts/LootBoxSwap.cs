using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxSwap : MonoBehaviour
{
    public LootBoxData lootBoxData;//empty//
    public LootBoxView lootBoxView;

    public GameObject casesPanel;
    public GameObject lootBoxMenu;
    private int slotMachineNumber;
    public GameObject boxInfo;
    public GameObject slotPanel;
    public GameObject slotMachinePrefab;

    public GameObject blocker;
    public GameObject arrowBack;

    public PlayerProfile profile;
    public Button openLootBoxButton;
    public List<SlotMachine> slotMachines=new List<SlotMachine>();
    private int currentStopped;
    public ProfilePanel profilePanel;
    private void Start()
    {
        blocker.SetActive(false);
    }
    public void SetLootBoxData(LootBoxData data)
    {
        casesPanel.SetActive(false);
        lootBoxMenu.SetActive(true);
        lootBoxData = data;

        lootBoxView.Show(lootBoxData);
    }
    public void SelectMachinesNumber (int amount)
    {
        slotMachineNumber = amount;

    }
    public void GenerateLootBoxes()
    {

        if (slotMachineNumber <= 0)
        {
            return;
        }
        profilePanel.Crystals -= slotMachineNumber * lootBoxData.price;
        blocker.SetActive(true);
        arrowBack.SetActive(false);

        boxInfo.SetActive(false);
        slotPanel.SetActive(true);
        for (int i = 0; i < slotMachineNumber; i++)
        {
            SlotMachine obj//here we instantiate this object
                           = Instantiate 
                (slotMachinePrefab, slotPanel.transform).GetComponent<SlotMachine>();//created copy
            obj.lootBoxData = lootBoxData;
            slotMachines.Add(obj);
            obj.OnComplete += SlotMachinesStop;
        }
        
    }
    private void SlotMachinesStop()
    {
        currentStopped++;
        if (currentStopped<slotMachineNumber)
        {
            return;
        }
        for (int i=slotMachines.Count-1; i>=0;i--)
        {
            SlotMachine obj = slotMachines[i];
            obj.OnComplete -= SlotMachinesStop;
            slotMachines.Remove(obj);
            Destroy(obj.gameObject);
            

        }
        blocker.SetActive(false);
        arrowBack.SetActive(true);

        boxInfo.SetActive(true);
        slotPanel.SetActive(false);
        slotMachineNumber = 0;
        currentStopped = 0;
    }
    private void Update()
    {
        if (slotMachineNumber == 0)
        {
            return;
        }
        if (lootBoxData.price*slotMachineNumber>profile.crystals)
        {
            openLootBoxButton.interactable = false;

        }
        else
        {
            openLootBoxButton.interactable = true;
            
        }
        
    }
}
