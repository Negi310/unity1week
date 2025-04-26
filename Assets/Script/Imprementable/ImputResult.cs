namespace ResultSystem
{
    public struct ImputResult
    {
        public float score;
        public float power;
        public float hammerSpeed;

        public ImputResult(float score, float power, float hammerSpeed)
        {
            this.score = score;
            this.power = power;
            this.hammerSpeed = hammerSpeed;
        }
    }
}