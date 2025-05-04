using UnityEngine;

public abstract class PhaseBase : MonoBehaviour, IPhase
{
    [SerializeField] protected BarBlock barBlock;
    [SerializeField] protected BarMoai barMoai;
    [SerializeField] protected GameObject[] targetPrefabs;
    [SerializeField] protected float blockBarDuration;
    [SerializeField] protected float moaiBarDuration;
    [SerializeField] protected int objectsPerPhase = 10;

    public virtual void Initialize()
    {
        if (barBlock != null)
        {
            barBlock.duration = blockBarDuration;
        }
        if (barMoai != null)
        {
            barMoai.duration = moaiBarDuration;
        }
    }

    public virtual GameObject GetNextSpawnObject()
    {
        return targetPrefabs[Random.Range(0, targetPrefabs.Length)];
    }

    public virtual int GetPerPhase() => objectsPerPhase;
}
