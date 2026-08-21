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
    void Start()
    {
        ArrowAnimation();
    }

    void Update()
    {
        fillBG.fillAmount = chance;
        chanceText.text= (chance*100).ToString()+'%';
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
    private async void ArrowAnimation()
    {
        float dur = Random.Range(spinDuration.x, spinDuration.y);
        float speed=Random.Range(startSpeed.x, startSpeed.y);

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
