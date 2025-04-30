using ResultSystem;

public static class ImputEvaluater
{
    public static ImputResult Evaluate(float distance)
    {
        int score = (int)distance;
        float power = distance;
        float hammerSpeed = distance;

        return new ImputResult(score, power, hammerSpeed);
    }
}
