using UnityEngine;

public class TargetDoubleMoai : TargetMoai
{
    private void FixedUpdate()
    {
        TrackCenter();
        CheckOutOfBounds();
        var hit = Physics2D.Raycast(transform.position, Vector2.down, td.rayLength, td.groundLayer);
        if (hit.collider == null || isLanded) return;
        EventBus.DoubleMoaiLanded(td.barDuration); // イベント発火！
        isLanded = true;
    }
}
