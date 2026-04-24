using System.CodeDom.Compiler;
using System.Collections.Generic;
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
    public AudioSource audio;
    private bool stopped;
    public PlayerProfile profile;
    void Start()
    {
        baseTime += Random.Range(-offsetTime, offsetTime);

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
                var winner =GetCenterSlot();
                profile.wonApps.Add(Instantiate(winner.appData));
                
            }
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
            float random = Random.Range(0,totalWeight) ;
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
        audio.PlayOneShot(audio.clip);
        slot.icon.sprite = data.icon;
        slot.appData= data;
        slot.transform.localPosition = new Vector3(0, 300, 0);
        createdApps.Add(slot);
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

