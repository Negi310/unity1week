using UnityEngine;

public class Phase1 : PhaseBase
{
    public override void Initialize()
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
}
