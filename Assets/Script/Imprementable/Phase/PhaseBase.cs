using UnityEngine;

public abstract class PhaseBase : MonoBehaviour, IPhase
{
    [SerializeField] protected BarBlock barBlock;
    [SerializeField] protected BarMoai barMoai;
    [SerializeField] protected GameObject[] targetPrefabs;
    [SerializeField] protected float blockBarDuration;
    [SerializeField] protected float moaiBarDuration;

    public abstract void Initialize();

    public virtual GameObject GetNextSpawnObject()
    {
        return targetPrefabs[Random.Range(0, targetPrefabs.Length)];
    }
}
