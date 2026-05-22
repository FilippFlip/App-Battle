using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvaluatorData", menuName = "Data/EvaluatorData")]
public class EvaluatorData : ScriptableObject
{
    public List<int> numbers = new();

    [HideInInspector] public string lastResult;

    public void Evaluate()
    {
        for (int i = 0; i < 5; i++)
        {
            int random = Random.Range(0, 10);
            numbers.Add(random);
        }
    }
}