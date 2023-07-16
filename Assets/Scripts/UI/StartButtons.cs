using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.UI
{
    public class StartButtons : MonoBehaviour
    {
        [SerializeField]
        private GameObject _start;
        [SerializeField]
        private GameObject _levelP;
        [SerializeField]
        private Button _playB;
        [SerializeField]
        public Button _shopB;
        [SerializeField]
        public Button _homeB;

        private void Start()
        {
            var gameStart = GameObject.Find("GameData").GetComponent<GameStart>();

            gameStart._startPanel = _start;
            gameStart._levelPanel = _levelP;
            gameStart._playButton = _playB;
            gameStart._shopButton = _shopB;
            gameStart._homeButton = _homeB;

            _playB.onClick.AddListener(gameStart.PlayGame);
            _shopB.onClick.AddListener(gameStart.Shop);
            _homeB.onClick.AddListener(gameStart.Home);
        }

    }
}
