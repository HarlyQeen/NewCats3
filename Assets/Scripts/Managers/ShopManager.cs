using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cats3.Managers
{
    public class ShopManager : MonoBehaviour
    {
        public int scene;
        private static ShopManager _instance;
        public GameObject _shopPanel;
        private AsyncOperation _loadingScene;

        private void Start()
        {
            _instance = this;
            _shopPanel.SetActive(false);


        }

        public void OpenShop()
        {
            _shopPanel.SetActive(true);
        }

        public void ExitShop()
        {
            _shopPanel.SetActive(false);

        }
        public void BuildHome()
        {
            _shopPanel.SetActive(false);

        }

        public void ExitToFloor()
        {
            _shopPanel.SetActive(false);

        }

        public void ExitScene()
        {
            _instance._loadingScene = SceneManager.LoadSceneAsync(scene);
        }
    }
}
