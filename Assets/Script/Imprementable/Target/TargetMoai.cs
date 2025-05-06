using UnityEngine;
using Cysharp.Threading.Tasks;

public class TargetMoai : TargetBase
{
    [SerializeField] private Transform moaiEye;

    [SerializeField] private LerpParams eyeParam;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.OnMoaiEyeGlow += HandleEyeGlow;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.OnMoaiEyeGlow -= HandleEyeGlow;
    }

    private void FixedUpdate()
    {
        TrackCenter();
        CheckOutOfBounds();
        var hit = Physics2D.Raycast(transform.position, Vector2.down, td.rayLength, td.groundLayer);
        if (hit.collider == null || isLanded) return;
        EventBus.MoaiLanded(td.barDuration); // イベント発火！
        isLanded = true;
    }

    private void HandleEyeGlow()
    {
        EyeGlow().Forget();
    }

    private async UniTask EyeGlow()
    {
        await UniTask.Yield();
    }
}
