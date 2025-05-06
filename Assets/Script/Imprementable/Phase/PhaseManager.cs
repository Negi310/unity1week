using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PhaseBase[] phases;

    private int currentPhaseIndex = 0;
    private int objectOutCount = 0;

    private Transform objPoint;

    private void OnEnable()
    {
        EventBus.OnRequestNextTarget += HandleObjectOut;
    }

    private void OnDisable()
    {
        EventBus.OnRequestNextTarget -= HandleObjectOut;
    }

    private void FixedUpdate()
    {
        TrackPos(spawnPoint);
    }

    private void HandleObjectOut()
    {
        objectOutCount++;
        if (objectOutCount >= phases[currentPhaseIndex].GetPerPhase())
        {
            objectOutCount = 0;
            currentPhaseIndex++;
            if (currentPhaseIndex > phases.Length)
            {
                return;
            }
        }
        SpawnNext();
    }

    private void SpawnNext()
    {
        var obj = phases[currentPhaseIndex].GetNextSpawnObject();
        GameObject target = Instantiate(obj, spawnPoint.position, Quaternion.identity);
        objPoint = target.transform;
    }

    private void TrackPos(Transform point)
    {
        if (objPoint == null) return;
        Vector2 pos = point.position;
        pos.x = objPoint.position.x;
        point.position = pos;
    }
}