using UnityEngine;

[CreateAssetMenu(fileName = "Appdata", menuName = "Data/Appdata")]
public class AppData : ScriptableObject
{
    public string appName;
    public int price;
    public Sprite icon;
    
}
