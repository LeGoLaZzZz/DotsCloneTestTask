using System;
using Model;
using TMPro;
using UnityEngine;
using Utils;

namespace ViewModel
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private string scoreFormat = "Счет: {0}";

        private ScoreCalculator _scoreCalculator;

        public void SetUp(ScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
            SetScoreText(_scoreCalculator.Score);
            _scoreCalculator.ScoreChangedEvent += OnScoreChanged;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefsController.SetScore(_scoreCalculator.Score); //couldnt detect ScoreCalculator destructor on exit playmode
        }

        private void OnScoreChanged(int score)
        {
            SetScoreText(score);
        }

        private void SetScoreText(int score)
        {
            scoreText.text = string.Format(scoreFormat, score);
        }
    }
}