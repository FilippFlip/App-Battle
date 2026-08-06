using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public AllAppData appData;
    public Transform availableContentHolder;
    public AppSlot rightAppSlot;
    void Start()
    {
        foreach(var entry in appData.apps)
        {
            if (entry.visibleInUpgrade==false)
            {
                continue;
            }
            var obj =Instantiate(rightAppSlot,availableContentHolder);
            obj.icon.sprite=entry.app.icon;
            obj.price.text=entry.app.price.ToString();
        }

    }

    void Update()
    {
        
    }
}
