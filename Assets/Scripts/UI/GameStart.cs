using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cats3.UI
{
    public class GameStart : MonoBehaviour
    {
        public int scene;
        private static GameStart _instance;
        public GameObject _startPanel;
        public GameObject _levelPanel;
        public Button _playButton;
        public Button _shopButton;
        public Button _homeButton;
        private AsyncOperation _loadingScene;

        private void Start()
        {

            _instance = this;
            _startPanel.SetActive(true);
            _levelPanel.SetActive(false);
        }


        public void PlayGame()
        {
            _startPanel.SetActive(false);
            _levelPanel.SetActive(true);
        }

        public void Home()
        {
            _startPanel.SetActive(true);
            _levelPanel.SetActive(false);
        }

        public void Shop()
        {
            _instance._loadingScene = SceneManager.LoadSceneAsync(scene);
            
        }
    }
}
