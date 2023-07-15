
using Cats3.Data;
using Cats3.ScriptableObjects;
using Cats3.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.Base
{
    public class Coins : MonoBehaviour
    {
      
        private GameData _gameData;
        public static int _allScores;
        public Text _allScoreText;

        private void OnEnable()
        {
            _gameData = FindObjectOfType<GameData>();
            LoadData();
            SetText();
        }
        void LoadData()
        {
            if (_gameData != null)
            {
                _allScores = CheckScores();
            }
        }

        void SetText()
        {
            _allScoreText.text = "" + _allScores;
        }


        private int CheckScores()
        {
            int scores =0;
            foreach (var score in _gameData._saveData._highScores)
            {
                scores += score;
            }
            return scores;
        }

    }
}

