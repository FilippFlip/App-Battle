using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public AllAppData appData;
    public Transform availableContentHolder;
    public AppSlot rightAppSlot;
    void Start()
    {
        foreach(var data in appData.apps)
        {
            var obj =Instantiate(rightAppSlot,availableContentHolder);
            obj.icon.sprite=data.icon;
            obj.price.text=data.price.ToString();
        }

    }

    void Update()
    {
        
    }
}
