using UnityEngine;

public abstract class PhaseBase : MonoBehaviour, IPhase
{
    [SerializeField] protected GameObject[] targetPrefabs;
    [SerializeField] protected int objectsPerPhase = 10;

    public virtual GameObject GetNextSpawnObject()
    {
        return targetPrefabs[Random.Range(0, targetPrefabs.Length)];
    }

    public virtual int GetPerPhase() => objectsPerPhase;
}
