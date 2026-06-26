using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SlotMachine : MonoBehaviour
{
    public LootBoxData lootBoxData;
    public float spawnRate;
    public float fallSpeed;
    public AppSlot slotPrefab;
    public Transform parent;
    private Dictionary<AppData, float> appChance= new Dictionary<AppData, float>();
    private float timer;
    private float timer2;
    private float totalWeight;
    public List<AppSlot> createdApps = new();
    public float baseTime;
    public float offsetTime;
    public AnimationCurve movingCurve;
    public float alignSpeed = 500f;
    public float endPause = 1f;
    private bool stopped;
    private bool completed;
    private AppSlot winner;
    public PlayerProfile profile;
    public event Action OnComplete;
    public event Action OnAppCreated;
    void Start()
    {
        baseTime += UnityEngine.Random.Range(-offsetTime, offsetTime);

        for(int i = 0; i < lootBoxData.lootBoxData.Length; i++)
        {
            totalWeight += lootBoxData.lootBoxData[i].weight;
            appChance.Add(lootBoxData.lootBoxData[i].item, totalWeight);
            
        }
       
    }
    
    void Update()
    {
        if (timer2 >= baseTime)
        {
            if (!stopped)
            {
                stopped = true;
                winner = GetCenterSlot();
            }
            AlignToWinner();
            return;
        }

        float value = movingCurve.Evaluate(timer2 / baseTime);

        for (int i = createdApps.Count-1;i >=0 ;i-- )
        {
            createdApps[i].transform.localPosition += Vector3.down * Time.deltaTime * fallSpeed*value;
            if (createdApps[i].transform.localPosition.y <= -350)
            {
                
                Destroy(createdApps[i].gameObject);
                createdApps.RemoveAt(i);
            }
        }
        
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer>spawnRate/value)
        {
            float random = UnityEngine.Random.Range(0,totalWeight) ;
            AppData data = null;
            foreach(var kv in appChance)
            {
                data = kv.Key;
                if (kv.Value>=random)
                {
                    break;
                }
            }
            GenerateIcon(data,parent);
            timer = 0;
            
        }
        
    }

    public void GenerateIcon(AppData data, Transform parent)
    {
        AppSlot slot = Instantiate(slotPrefab, parent);
        OnAppCreated?.Invoke();
        slot.icon.sprite = data.icon;
        slot.appData= data;
        slot.transform.localPosition = new Vector3(0, 300, 0);
        createdApps.Add(slot);
    }
    private void AlignToWinner()
    {
        if (completed)
        {
            return;
        }
        if (winner == null)
        {
            Finish();
            return;
        }

        float y = winner.transform.localPosition.y;
        float step = alignSpeed * Time.deltaTime;

        // Дошли до цели в пределах одного шага — ставим победителя ровно на 0 и завершаем.
        if (Mathf.Abs(y) <= step)
        {
            MoveAllSlots(-y);
            Finish();
            return;
        }

        // Двигаем все слоты к нулю: y>0 — вниз, y<0 — вверх.
        MoveAllSlots(-Mathf.Sign(y) * step);
    }

    private void MoveAllSlots(float deltaY)
    {
        for (int i = 0; i < createdApps.Count; i++)
        {
            createdApps[i].transform.localPosition += Vector3.up * deltaY;
        }
    }

    private async void Finish()
    {
        completed = true;
        if (winner != null)
        {
            profile.AddItem(winner.appData);
        }
        // Небольшая пауза, чтобы игрок успел увидеть победителя по центру.
        await Awaitable.WaitForSecondsAsync(endPause);
        OnComplete?.Invoke();
    }

    public AppSlot GetCenterSlot()
    {
        AppSlot closest = null;
        float minDistance = float.MaxValue;

        foreach (var slot in createdApps)
        {
            float dist = Mathf.Abs(slot.transform.localPosition.y);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = slot;
            }
        }
        return closest;
    }

}

