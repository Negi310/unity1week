using ResultSystem;

public static class ImputEvaluater
{
    public static ImputResult Evaluate(float distance)
    {
        float score = distance;
        float power = distance;
        float hammerSpeed = distance;

        return new ImputResult(score, power, hammerSpeed);
    }
}
