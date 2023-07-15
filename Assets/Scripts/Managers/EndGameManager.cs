using Cats3.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Cats3.Managers
{
    public enum GameType
    {
        Moves,
        Time
    }

    [System.Serializable]
    public class EndGameRequirements
    {
        public GameType _gameType;
        public int _counterValue;
    }
    public class EndGameManager : MonoBehaviour
    {
        public GameObject _movesLabel;
        public GameObject _timeLabel;
        public GameObject _tryAgainMenu;
        public GameObject _youWinMenu;
        public Text _counter;
        public EndGameRequirements _requirements;
        public int _currentCounter;
        private Board _board;
        private float _timer;

        private void Start()
        {
            _board = FindObjectOfType<Board>();
            SetGameType();
            SetupGame();
        }

        void SetGameType()
        {
            if(_board._world != null)
            {
                if (_board._level < _board._world._levels.Length)
                {
                    if (_board._world._levels[_board._level] != null)
                    {
                        _requirements = _board._world._levels[_board._level]._endRequirements;
                    }
                }
            }
                
        }

        public void SetupGame()
        {
            _currentCounter = _requirements._counterValue;
            if (_requirements._gameType == GameType.Moves)
            {
                _movesLabel.SetActive(true);
                _timeLabel.SetActive(false);
            }
            else
            {
                _timer = 1;
                _movesLabel.SetActive(false);
                _timeLabel.SetActive(true);
            }
            _counter.text = "" + _currentCounter;
        }

        public void DecreaseCounter() //Уменьшение ходов/времени
        {
            if (_board._currentState != GameState.pause)
            {
                _currentCounter--;
                _counter.text = "" + _currentCounter;
                if (_currentCounter <= 0)
                {
                    LoseGame();
                }
            }
        }

        public void WinGame()
        {
            _youWinMenu.SetActive(true);
            _board._currentState = GameState.win;
            _currentCounter = 0;
            _counter.text = "" + _currentCounter;
            FadePanelController fade = FindObjectOfType<FadePanelController>();
            fade.GameOver();

        }

        public void LoseGame()
        {
            _tryAgainMenu.SetActive(true);
            _board._currentState = GameState.lose;
            Debug.Log("Ты проиграл!!!");
            _currentCounter = 0;
            _counter.text = "" + _currentCounter;
            FadePanelController fade = FindObjectOfType<FadePanelController>();
            fade.GameOver();
        }

        private void Update()
        {
            if (_requirements._gameType == GameType.Time && _currentCounter > 0)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    DecreaseCounter();
                    _timer = 1;
                }
            }
        }
    }
}
