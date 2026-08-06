using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllAppdata", menuName = "Data/AllAppdata")]
public class AllAppData : ScriptableObject
{
    public List<AppEntry> apps=new();

}

[Serializable]
public class AppEntry
{
    public AppData app;
    public bool visibleInUpgrade = true;
}
