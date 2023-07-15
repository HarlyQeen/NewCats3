using Cats3.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.UI
{
    public class LevelButton : MonoBehaviour
    {
        [Header("Активные")]
        public bool _isActive;
        public Sprite _activeSprite;
        public Sprite _lockedSprite;
        private Image _buttonImage;
        private Button _myButton;
        private int _starsActive;

        [Header("UI Уровня")]
        public Image[] _stars;
        public Text _levelText;
        public int _level;
        public GameObject _confirmPanel;
       

        private GameData _gameData;

        private void Start()
        {
            _gameData = FindObjectOfType<GameData>();
            _buttonImage = GetComponent<Image>();
            _myButton = GetComponent<Button>();
            LoadData();
            ActiveStars();
            ShowLevel();
            DecideSprite();
        }

        void LoadData()
        {
            //Грузится ли GameData
            if(_gameData != null)
            {
                //Сделаем уровень активным
                if(_gameData._saveData._isActive[_level])
                {
                    _isActive = true;
                }
                else
                {
                    _isActive = false;
                }
                //Сколько звезд активируем
                _starsActive = _gameData._saveData._stars[_level];
            }
        }

        void ActiveStars()
        {
            for( int i = 0; i < _starsActive; i++)
            {

                _stars[i].enabled = true;
            }
        }

        void DecideSprite() //выберем спрайт
        {
            if(_isActive)
            {
                _buttonImage.sprite = _activeSprite;
                _myButton.enabled = true;
                _levelText.enabled = true;
            }
            else
            {
                _buttonImage.sprite = _lockedSprite;
                _myButton.enabled = false;
                _levelText.enabled = false;
            }
        }

        void ShowLevel()
        {
            _levelText.text = "" + _level;
        }

        public void ConfirmPanel(int _level)
        {
            _gameData.Save();
            _confirmPanel.GetComponent<ConfirmPanel>()._level = _level;
            _confirmPanel.SetActive(true);

        }
    }
}
