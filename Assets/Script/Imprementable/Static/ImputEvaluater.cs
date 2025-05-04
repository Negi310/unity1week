using ResultSystem;
using UnityEngine;

public class ImputEvaluater : MonoBehaviour
{
    public static ImputEvaluater I;

    public AnimationCurve scoreCurve;
    
    public AnimationCurve powerCurve;
    
    public AnimationCurve hammerSpeedCurve;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        };
    }

    public ImputResult Evaluate(float distance)
    {
        int score = 1000 * (int)scoreCurve.Evaluate(distance);
        float power = distance;
        float hammerSpeed = 100f * hammerSpeedCurve.Evaluate(distance);

        return new ImputResult(score, power, hammerSpeed);
    }
}
