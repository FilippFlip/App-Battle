using UnityEngine;

public class AppIconGenerator : MonoBehaviour
{
    public Transform profileSpace;
    public AppSlot slotPrefab;
    public AppData[] appDatas;
 
    void Update()
    {
        
    }
    public void GenerateIcon (AppData data,Transform parent) 
    {
        AppSlot slot = Instantiate(slotPrefab,parent);
        slot.icon.sprite = data.icon;
        slot.price.text=data.price.ToString();

    } 
    
}
