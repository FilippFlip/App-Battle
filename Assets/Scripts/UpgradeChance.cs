using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeChance : MonoBehaviour
{
    [Range(0,100)]
    public float chance;
    public Image arrow;
    public TMP_Text chanceText;
    public Image fillBG;
    public bool hit;
    void Start()
    {
        
    }

    void Update()
    {
        fillBG.fillAmount = chance/100;
        chanceText.text= chance.ToString()+'%';
        hit = ArrowHit();
    }
    private bool ArrowHit()
    {
        float range1=0;
        float range2=360;

        float x = 180 * (100 - chance) / 100;
        range1 += x;
        range2 -= x;
        float arrowAngle=arrow.transform.rotation.eulerAngles.z;
        if (arrowAngle > range1 && arrowAngle < range2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
