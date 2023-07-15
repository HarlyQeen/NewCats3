using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Base
{
    public class CameraScaler : MonoBehaviour
    {
        private Board _board;
        public float _cameraOffset;
        public float aspectRatio = 0.5625f;
        public float padding = 2;
        public float yOffset = 1;

        void Start()
        {
            _board = FindObjectOfType<Board>();
            if (_board != null)
            {
                RepositionCamera(_board._width - 1, _board._height - 1);
            }
        }

        void RepositionCamera(float x, float y)
        {
            Vector3 tempPosition = new Vector3(x / 2, y / 2 + yOffset, _cameraOffset);
            transform.position = tempPosition;
            if (_board._width >= _board._height)
            {
                Camera.main.orthographicSize = (_board._width / 2 + padding) / aspectRatio;
            }
            else
            {
                Camera.main.orthographicSize = _board._height / 2 + padding;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
