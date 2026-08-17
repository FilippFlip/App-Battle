using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UpgradeManager : MonoBehaviour
{
    public AllAppData appData;
    public Transform rightContent;
    public Transform leftContent;
    public AppSlot rightAppSlot;
   
    public PlayerProfile profile;
    void Start()
    {
        foreach (var entry in appData.apps.OrderBy(entry => entry.app.price)) 
        {
            if (entry.visibleInUpgrade==false)
            {
                continue;
            }
            var obj =Instantiate(rightAppSlot,rightContent);
            obj.icon.sprite=entry.app.icon;
            obj.price.text=entry.app.price.ToString();
        }
        foreach (var app in profile.wonApps)
        {
            var obj = Instantiate(rightAppSlot, leftContent);
            obj.icon.sprite = app.icon;
            obj.price.text = app.price.ToString();
        }
    }

    void Update()
    {
        
    }
}
