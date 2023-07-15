using Cats3.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.UI
{
    public class LevelSelect : MonoBehaviour
    {
        public GameObject[] _panels;
        public GameObject _currentPanel;
        public int _page;
        private GameData _gameData;
        public int currentLevel = 1;

        private void Start()
        {
            _gameData = FindObjectOfType<GameData>();
            for(int i = 0; i < _panels.Length; i++)
            {
                _panels[i].SetActive(false);
            }
            if(_gameData != null)
            {
                for(int i = 0; i < _gameData._saveData._isActive.Count; i++)
                {
                    if(_gameData._saveData._isActive[i])
                    {
                        currentLevel = i;
                    }
                }
            }
            _page = (int)Mathf.Floor(currentLevel / 9);
            _currentPanel = _panels[_page];
            _panels[_page].SetActive(true);   
        }
        public void PageToRight()
        {
            if (_page < _panels.Length - 1)
            {
                _currentPanel.SetActive(false);
                _page++;
                _currentPanel = _panels[_page];
                _currentPanel.SetActive(true);
            }
        }

        public void PageToLeft()
        {
            if (_page > 0)
            {
                _currentPanel.SetActive(false);
                _page--;
                _currentPanel = _panels[_page];
                _currentPanel.SetActive(true);
            }
        }
    }
}
