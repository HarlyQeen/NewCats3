using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Cats3.UI
{
    public class GameStart : MonoBehaviour
    {
        public GameObject _startPanel;
        public GameObject _levelPanel;

        private void Start()
        {
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
            SceneManager.LoadScene("Shop");
        }
    }
}
