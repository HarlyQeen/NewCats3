using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.Base
{
    public class GoalPanel : MonoBehaviour
    {
        public Image _thisImage;
        public Sprite _thisSprite;
        public Text _thisText;
        public string _thisString;

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            _thisImage.sprite = _thisSprite;
            _thisText.text = _thisString;
        }

    }
}
