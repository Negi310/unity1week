using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PhaseBase[] phases;

    private int currentPhaseIndex = 0;
    private int objectOutCount = 0;
    [SerializeField] private int objectsPerPhase = 10;

    private void OnEnable()
    {
        EventBus.OnRequestNextTarget += HandleObjectOut;
    }

    private void OnDisable()
    {
        EventBus.OnRequestNextTarget -= HandleObjectOut;
    }

    private void Start()
    {
        phases[currentPhaseIndex].Initialize();
        SpawnNext();
    }

    private void HandleObjectOut()
    {
        objectOutCount++;
        if (objectOutCount >= objectsPerPhase)
        {
            objectOutCount = 0;
            currentPhaseIndex++;
            if (currentPhaseIndex < phases.Length)
            {
                phases[currentPhaseIndex].Initialize();
            }
            else
            {
                Debug.Log("全フェーズ終了");
                return;
            }
        }
        SpawnNext();
    }

    private void SpawnNext()
    {
        var obj = phases[currentPhaseIndex].GetNextSpawnObject();
        Instantiate(obj, spawnPoint.position, Quaternion.identity);
    }
}