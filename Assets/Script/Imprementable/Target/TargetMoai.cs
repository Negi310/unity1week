using UnityEngine;

public class TargetMoai : TargetBase
{
    private void FixedUpdate()
    {
        CheckOutOfBounds();
        var hit = Physics2D.Raycast(transform.position, Vector2.down, td.rayLength, td.groundLayer);
        if (hit.collider == null) return;
        EventBus.MoaiLanded(); // イベント発火！
    }
}
