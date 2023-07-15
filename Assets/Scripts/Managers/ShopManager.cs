using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cats3.Managers
{
    public class ShopManager : MonoBehaviour
    {
        public GameObject _shopPanel;
        public GameObject _roomCam;
        public GameObject _Main2D;

        private void Start()
        {
            _shopPanel.SetActive(false);
            _Main2D.SetActive(true);
            _roomCam.SetActive(false);

        }

        public void OpenShop()
        {
            _shopPanel.SetActive(true);
            _Main2D.SetActive(true);
            _roomCam.SetActive(false);
        }

        public void ExitShop()
        {
            _shopPanel.SetActive(false);
            _Main2D.SetActive(true);
            _roomCam.SetActive(false);

        }
        public void BuildHome()
        {
            _shopPanel.SetActive(false);
            _Main2D.SetActive(false);
            _roomCam.SetActive(true);
        }

        public void ExitToFloor()
        {
            _shopPanel.SetActive(false);
            _Main2D.SetActive(true);
            _roomCam.SetActive(false);
        }

        public void ExitScene()
        {
            SceneManager.LoadScene("LevelWorld");
        }
    }
}
