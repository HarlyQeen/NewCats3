using Cats3.Base;
using Cats3.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cats3.UI
{
    public class BackToLevelWorld : MonoBehaviour
    {
        public string _sceneToLoad;
        private GameData _gameData;
        private Board _board;

        public void WinOKButton()
        {
            if(_gameData != null)
            {
                _gameData._saveData._isActive[_board._level +1] = true;
                _gameData.Save();
            }
            SceneManager.LoadScene(_sceneToLoad);
        }

        public void LoseOKButton()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }

        private void Start()
        {
            _gameData = FindObjectOfType<GameData>();
            _board = FindObjectOfType<Board>();
        }
    }
}
