using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetBlock : TargetBase
{
    private void FixedUpdate()
    {
        TrackCenter();
        CheckOutOfBounds();
        var hit = Physics2D.Raycast(transform.position, Vector2.down, td.rayLength, td.groundLayer);
        if (hit.collider == null || isLanded) return;
        EventBus.BlockLanded(); // イベント発火！
        isLanded = true;
    }
}
