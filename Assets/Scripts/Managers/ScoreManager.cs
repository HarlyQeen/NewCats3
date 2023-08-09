using Cats3.Base;
using Cats3.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public Board _board;
        public Text _scoreText;
        public int _score;
        public Image _scoreBar;
        private GameData _gameData;
        private int _numberStars;


        void Start()
        {
            _board = FindObjectOfType<Board>();
            _gameData = FindObjectOfType<GameData>();
            UpdateBar();
            if(_gameData != null)
            {
                _gameData.Load();
            } 
        }

        void Update()
        {
            _scoreText.text = "" + _score;
        }

        public void CounterScore(int amountToUp)
        {
            _score += amountToUp;
            
            for (int i = 0; i < _board._scoreGoals.Length; i++)
            {
                if(_score > _board._scoreGoals[i] && _numberStars < i + 1)
                {
                    _numberStars++;
                }
            }
            if (_gameData != null)
            {
                int _highScore = _gameData._saveData._highScores[_board._level];

                if (_score > _highScore)
                {
                    _gameData._saveData._highScores[_board._level] = _score;
                    int currentStars = _gameData._saveData._stars[_board._level];
                    if(_numberStars > currentStars)
                    {
                        _gameData._saveData._stars[_board._level] = _numberStars;
                    }
                }
                _gameData.Save();
            }
            UpdateBar();
        }


        void UpdateBar()
        {
            if (_board != null && _scoreBar != null)
            {
                int length = _board._scoreGoals.Length;
                _scoreBar.fillAmount = (float)_score / (float)_board._scoreGoals[length - 1];
            }
        }
    }
}


