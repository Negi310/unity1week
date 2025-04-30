namespace ResultSystem
{
    public struct ScoreResult
    {
        public int scores;
        public int score;
        public int comboCount;

        public ScoreResult(int scores, int score, int comboCount)
        {
            this.scores = scores;
            this.score = score;
            this.comboCount = comboCount;
        }
    }
}