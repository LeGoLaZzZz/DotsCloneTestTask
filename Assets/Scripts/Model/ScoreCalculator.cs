using System;
using UnityEngine;
using Utils;

namespace Model
{
    public class ScoreCalculator
    {
        private int _score;
        private CellConnector _cellConnector;

        public event Action<int> ScoreChangedEvent;
        public int Score => _score;

        public ScoreCalculator(CellConnector cellConnector)
        {
            _cellConnector = cellConnector;
            _score = PlayerPrefsController.GetScore();
            _cellConnector.ChipsRemovedEvent += OnChipsRemoved;
        }

        ~ScoreCalculator()
        {
            _cellConnector.ChipsRemovedEvent -= OnChipsRemoved;
        }

        private void OnChipsRemoved(int removed)
        {
            _score += removed;
            ScoreChangedEvent?.Invoke(_score);
        }

    }
}