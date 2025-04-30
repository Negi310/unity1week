namespace ResultSystem
{
    public struct ImputResult
    {
        public int score;
        public float power;
        public float hammerSpeed;

        public ImputResult(int score, float power, float hammerSpeed)
        {
            this.score = score;
            this.power = power;
            this.hammerSpeed = hammerSpeed;
        }
    }
}