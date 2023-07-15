using Cats3.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Managers
{
    public class HintManager : MonoBehaviour
    {
        private Board _board;
        public float _hintDelay;
        private float _hintDelaySeconds;
        public GameObject _hintParticle;
        public GameObject _currentHint;


        void Start()
        {
            _board = FindObjectOfType<Board>();
            _hintDelaySeconds = _hintDelay;

        }


        void Update()
        {
            _hintDelaySeconds -= Time.deltaTime * 2;
            if (_hintDelaySeconds <= 0 && _currentHint == null)
            {
                MarkHint();
                _hintDelaySeconds = _hintDelay;
            }
        }

        //Я хочу найти все совпадения на столе
        List<GameObject> FindAllMatches()
        {
            List<GameObject> possibleMoves = new List<GameObject>();
            for (int i = 0; i < _board._width; i++)
            {
                for (int j = 0; j < _board._height; j++)
                {
                    if (_board._allCats[i, j] != null)
                    {
                        if (i < _board._width - 1)
                        {
                            if (_board.SwitchAndCheck(i, j, Vector2.right))
                            {
                                possibleMoves.Add(_board._allCats[i, j]);
                            }
                        }

                        if (j < _board._height - 1)
                        {
                            if (_board.SwitchAndCheck(i, j, Vector2.up))
                            {
                                possibleMoves.Add(_board._allCats[i, j]);
                            }
                        }
                    }
                }
            }
            return possibleMoves;
        }
        //выбрать одно рондомное совпадение
        GameObject PickOneRandom()
        {
            List<GameObject> possibleMoves = new List<GameObject>();
            possibleMoves = FindAllMatches();
            if (possibleMoves.Count > 0)
            {
                int pieceToUse = Random.Range(0, possibleMoves.Count);
                return possibleMoves[pieceToUse];
            }
            return null;
        }
        //сгенерировать подсказку
        private void MarkHint()
        {
            GameObject move = PickOneRandom();
            if (move != null)
            {
                _currentHint = Instantiate(_hintParticle, move.transform.position, Quaternion.identity);
            }
        }
        //убрать подсказку

        public void DestroyHint()
        {
            if (_currentHint != null)
            {
                Destroy(_currentHint);
                _currentHint = null;
                _hintDelaySeconds = _hintDelay;
            }
        }
    }
}
