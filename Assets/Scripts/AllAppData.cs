using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllAppdata", menuName = "Data/AllAppdata")]
public class AllAppData : ScriptableObject
{
    public List<AppData> apps=new();

}
