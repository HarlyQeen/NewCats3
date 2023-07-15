using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cats3.Base;
using Cats3.Managers;

namespace Cats3.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject _pausePanel;
        private Board _board;
        public bool paused = false;
        public Image _soundButton;
        public Sprite _musicOn;
        public Sprite _musicOff;
        private SoundManager _sound;
        


        private void Start()
        {
            _sound = FindObjectOfType<SoundManager>();
            _pausePanel.SetActive(false);
            _board = FindObjectOfType<Board>();

            //Если звук == 0 - без звука, если == 1 - звук включен
            if (PlayerPrefs.HasKey("Звук"))
            {
                if(PlayerPrefs.GetInt("Звук") == 0)
                {
                    _soundButton.sprite = _musicOff;
                }
                else 
                {
                    _soundButton.sprite = _musicOn;
                }
            }
            else
            {
                _soundButton.sprite = _musicOn;
            }
            
        }

        void Update()
        {
            if (paused && !_pausePanel.activeInHierarchy)
            {
                _pausePanel.SetActive(true);
                _board._currentState = GameState.pause;
            }
            if (!paused && _pausePanel.activeInHierarchy)
            {
                _pausePanel.SetActive(false);
                _board._currentState = GameState.move;
            }

        }

        public void SoundButton()
        {
            if (PlayerPrefs.HasKey("Звук"))
            {
                if (PlayerPrefs.GetInt("Звук") == 0)
                {
                    PlayerPrefs.SetInt("Звук", 1);
                    _soundButton.sprite = _musicOn;
                    _sound.Volume();
                }
                else
                { 
                    PlayerPrefs.SetInt("Звук", 0);
                    _soundButton.sprite = _musicOff;
                    _sound.Volume();
                }
            }
            else
            {
                PlayerPrefs.SetInt("Звук", 1);
                _soundButton.sprite = _musicOff;
                _sound.Volume();
            }
        }

        public void PauseGame()
        {
            paused = !paused;
        }

        public void ExitGame()
        {
            SceneManager.LoadScene("LevelWorld");
        }
    }
}
