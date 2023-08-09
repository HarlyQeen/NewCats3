using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cats3.Data;
using Cats3.Base;

namespace Cats3.UI
{
    public class ConfirmPanel : MonoBehaviour
    {
        [Header("информация об уровне")]
        public string _levelToLoad;
        public int _level;
        private GameData _gameData;
        private int _starsActive;
        public int _highScore;

        [Header("UI")]
        public Image[] _stars;
        public Text _highScoreText;
        public Text _starsText;


        private void OnEnable()
        {
            _gameData = FindObjectOfType<GameData>();
            LoadData();
            ActiveStars();
            SetText();
        }


        void LoadData()
        {
            if(_gameData != null)
            {
                _starsActive = _gameData._saveData._stars[_level];
                _highScore = _gameData._saveData._highScores[_level];

            }
        }

        void SetText()
        {
            _highScoreText.text = "" + _highScore;
            _starsText.text = "" + _starsActive + "/3";
        }

        void ActiveStars()
        {
            for (int i = 0; i < _starsActive; i++)
            {
                _stars[i].enabled = true;
            }
        }

        public void Cancel()
        {
            this.gameObject.SetActive(false);
        }
        
        public void Play()
        {
            PlayerPrefs.SetInt("Выбор уровня", _level);
            SceneManager.LoadScene(_levelToLoad);
        }
    }
}
