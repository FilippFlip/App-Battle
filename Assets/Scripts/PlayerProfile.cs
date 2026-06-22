using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Scriptable Objects/PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    public float crystals;
    public string playerName;
    public List<AppData> wonApps = new();
    public Action<AppData> OnItemAdded;
    public Action<AppData> OnItemRemoved;
    public void AddItem(AppData item)
    {
        OnItemAdded?.Invoke(item);
        wonApps.Add(item);
    }
    public void RemoveItem(AppData item)
    {
        if (wonApps.Remove(item))
            OnItemRemoved?.Invoke(item);
    }
}
