using System;
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

    public Vector2 spinDuration;
    public Vector2 startSpeed;
    public AnimationCurve curve;
    private float progress;
    

    void Update()
    {
        fillBG.fillAmount = chance;
        int digits = 3;
        if (chance > 0)
        {
            digits = Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(chance)), 3, 15);
        }
        chanceText.text = (chance * 100f).ToString("0." + new string('#', digits - 2)) + "%";
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
    public async Awaitable ArrowAnimation()
    {
        float dur = UnityEngine.Random.Range(spinDuration.x, spinDuration.y);
        float speed= UnityEngine.Random.Range(startSpeed.x, startSpeed.y);

        float time = 0;
        float curSpeed;
        while (progress <= 1)
        {
            time += Time.deltaTime;
            progress = time / dur;
            curSpeed = curve.Evaluate(progress) * speed * Time.deltaTime;
            arrow.transform.Rotate(0, 0, -curSpeed);
            await Awaitable.NextFrameAsync();

        }
    }
}
