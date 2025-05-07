using UnityEngine;

public class TargetDoubleBlock : TargetBase
{
    public void FixedUpdate()
    {
        TrackCenter();
        CheckOutOfBounds();
        var hit = Physics2D.Raycast(transform.position, Vector2.down, td.rayLength, td.groundLayer);
        if (hit.collider == null || isLanded) return;
        EventBus.DoubleBlockLanded(td.barDuration); // イベント発火！
        isLanded = true;
    }
}
