using UnityEngine;

namespace Utils
{
    public static class PlayerPrefsController
    {
        private const string ScoreKey = "scoreKey";

        public static void SetScore(int score) => PlayerPrefs.SetInt(ScoreKey, score);
        public static int GetScore() => PlayerPrefs.GetInt(ScoreKey,0);
    }
}