using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Phase", menuName = "Scriptable Objects/Phase")]
public class PhaseBase : ScriptableObject, IPhase
{
    [SerializeField] protected List<PhaseTarget> targetPrefabs;
    [SerializeField] protected float correctionFactor = 1f;
    [SerializeField] protected int objectsPerPhase = 10;

    public virtual GameObject GetNextSpawnObject()
    {
        if (targetPrefabs.Count == 0) return null;

        var totalWeight = 0f;
        var adjustedWeights = new List<float>();

        foreach (var wp in targetPrefabs)
        {
            float adjustedWeight = wp.baseWeight * (1f + correctionFactor / (1f + wp.appearCount));
            adjustedWeights.Add(adjustedWeight);
            totalWeight += adjustedWeight;
        }

        float r = Random.Range(0f, totalWeight);
        var sum = 0f;
        for (int i = 0; i < targetPrefabs.Count; i++)
        {
            sum += adjustedWeights[i];
            if (r <= sum)
            {
                targetPrefabs[i].appearCount++;
                return targetPrefabs[i].prefab;
            }
        }

        return targetPrefabs[0].prefab; // フォールバック
    }

    public virtual int GetPerPhase() => objectsPerPhase;
}
