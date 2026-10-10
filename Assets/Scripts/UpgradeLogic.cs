using UnityEngine;
using UnityEngine.UI;

public class UpgradeLogic : MonoBehaviour
{
    public UpgradeManager manager;
    public Button upgradeButton;
    public UpgradeChance upChance;
    public PlayerProfile playerProfile;
    void Update()
    {
        if (manager.rSlot != null && manager.lSlot != null)
        {
            float chance = (float)manager.lSlot.price / (float)manager.rSlot.price;
            upChance.chance = chance;
            upgradeButton.interactable = true;
        }
        else
        {
            upChance.chance = 0;
            upgradeButton.interactable = false;
        }
    }
    public async void Upgrade()
    {
        await upChance.ArrowAnimation();
        if (upChance.hit)
        {
            playerProfile.AddItem(manager.rSlot);
        }
        else
        {
            playerProfile.RemoveItem(manager.lSlot);
        }
        manager.UpdateLeftContentSlot();
        manager.UpdateRightContentSlot();
    }
}
