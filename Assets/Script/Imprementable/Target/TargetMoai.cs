using UnityEngine;
using Cysharp.Threading.Tasks;

public class TargetMoai : TargetBase
{
    public Transform moaiEye;

    public LerpParams eyeParam;

    public override void OnEnable()
    {
        base.OnEnable();
        EventBus.OnMoaiEyeGlow += HandleEyeGlow;
    }

    public override void OnDisable()
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

    public virtual void HandleEyeGlow()
    {
        if (!isLanded || GameManager.I.CurrentState != GameState.Playing) return;
        EyeGlow().Forget();
    }

    public async UniTask EyeGlow()
    {
        await eyeParam.RunLerp(value => moaiEye.localScale = (Vector3)value);
    }

    public override void TrackCenter()
    {
        TrackDisable();
        if (isLanded || isntTrack ) return;
        Vector2 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, 0f, 1.5f * Time.fixedDeltaTime);
        transform.position = pos;
    }
}
