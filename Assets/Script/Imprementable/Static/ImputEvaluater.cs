using ResultSystem;
using UnityEngine;

public class ImputEvaluater : MonoBehaviour
{
    public static ImputEvaluater I;

    public AnimationCurve scoreCurve;

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
        int score = (int)(1000f * scoreCurve.Evaluate(distance));
        float power = distance;
        float hammerSpeed = distance;

        return new ImputResult(score, power, hammerSpeed);
    }
}
