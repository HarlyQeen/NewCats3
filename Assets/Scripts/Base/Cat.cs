using Cats3.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Base
{
    public class Cat : MonoBehaviour
    {
        [Header("Окно вариаций")]
        public int _column;
        public int _row;
        public int _previousColumn;
        public int _previousRow;
        public int _targetX;
        public int _targetY;
        public bool isMatched = false;

        private EndGameManager _endGameManager;
        private HintManager _hintManager;
        private FindMatches _findMatches;
        private Board _board;
        public GameObject _otherCat;
        public Vector2 _firstTouchPosition;
        public Vector2 _finalTouchPosition;
        public Vector2 _tempPosition;

        [Header("Проведите пальцем")]
        public float swipeAngle = 0;
        public float swipeResist = 1f;

        [Header("Бонусы")]
        public bool _isColorBomb;
        public bool _isColumnBomb;
        public bool _isRowBomb;
        public bool _isExplBomb;
        public GameObject _explBomb;
        public GameObject _rowArrow;
        public GameObject _columnArrow;
        public GameObject _colorBomb;

        void Start()
        {
            _isColumnBomb = false;
            _isRowBomb = false;
            _isColorBomb = false;
            _isExplBomb = false;

            _endGameManager = FindObjectOfType<EndGameManager>();
            _hintManager = FindObjectOfType<HintManager>();
            _board = FindObjectOfType<Board>();
            _findMatches = FindObjectOfType<FindMatches>();
            /*_targetX = (int)transform.position.x;
            _targetY = (int)transform.position.y;
            _row = _targetY;
            _column = _targetX;
            _previousRow = _row;
            _previousColumn = _column;*/
        }

        //Это только для теста и ошибок
        /*private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _isExplBomb = true;
                GameObject marker = Instantiate(_explBomb, transform.position, Quaternion.identity);
                marker.transform.parent = this.transform;
            }
        }*/

        void Update()
        {
            _targetX = _column;
            _targetY = _row;
            if (Mathf.Abs(_targetX - transform.position.x) > .1)
            {
                //Двигаемся к цели
                _tempPosition = new Vector2(_targetX, transform.position.y);
                transform.position = Vector2.Lerp(transform.position, _tempPosition, .6f);
                if (_board._allCats[_column, _row] != this.gameObject)
                {
                    _board._allCats[_column, _row] = this.gameObject;
                    _findMatches.FindAllMatches();
                }
                
            }
            else
            {
                //Установленное положение
                _tempPosition = new Vector2(_targetX, transform.position.y);
                transform.position = _tempPosition;

            }
            if (Mathf.Abs(_targetY - transform.position.y) > .1)
            {
                //Двигаемся к цели
                _tempPosition = new Vector2(transform.position.x, _targetY);
                transform.position = Vector2.Lerp(transform.position, _tempPosition, .6f);
                if (_board._allCats[_column, _row] != this.gameObject)
                {
                    _board._allCats[_column, _row] = this.gameObject;
                    _findMatches.FindAllMatches();
                }
               
            }
            else
            {
                //Установленное положение
                _tempPosition = new Vector2(transform.position.x, _targetY);
                transform.position = _tempPosition;
            }
        }

        public IEnumerator CheckMoveCoroutine()
        {
            if (_colorBomb)
            {
                //Эта чать - цветная бомба, а другая - уничтожения
                _findMatches.MatchPiecesOfColor(_otherCat.tag);
                isMatched = true;
            }
            else if (_otherCat.GetComponent<Cat>()._isColorBomb)
            {
                //Эта чать - уничтожения цвета, а другая - цветная бомба
                _findMatches.MatchPiecesOfColor(this.gameObject.tag);
                _otherCat.GetComponent<Cat>().isMatched = true;
            }
            yield return new WaitForSeconds(.5f);
            if (_otherCat != null)
            {
                if (!isMatched && !_otherCat.GetComponent<Cat>().isMatched)
                {
                    _otherCat.GetComponent<Cat>()._row = _row;
                    _otherCat.GetComponent<Cat>()._column = _column;
                    _row = _previousRow;
                    _column = _previousColumn;
                    yield return new WaitForSeconds(.5f);
                    _board._currentCat = null;
                    _board._currentState = GameState.move;
                }
                else
                {
                    if (_endGameManager != null)
                    {
                        if (_endGameManager._requirements._gameType == GameType.Moves)
                        {
                            _endGameManager.DecreaseCounter();
                        }
                    }
                    _board.DestroyMatches();

                }
                //_otherCat = null;
            }

        }

        private void OnMouseDown()
        {
            //убрать подсказку
            if (_hintManager != null)
            {
                _hintManager.DestroyHint();
            }


            if (_board._currentState == GameState.move)
            {
                _firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }

        private void OnMouseUp()
        {
            if (_board._currentState == GameState.move)
            {
                _finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CalculateAngle();
            }

        }

        void CalculateAngle()
        {
            if (Mathf.Abs(_finalTouchPosition.y - _firstTouchPosition.y) > swipeResist || Mathf.Abs(_finalTouchPosition.x - _firstTouchPosition.x) > swipeResist)
            {
                _board._currentState = GameState.wait;
                swipeAngle = Mathf.Atan2(_finalTouchPosition.y - _firstTouchPosition.y, _finalTouchPosition.x - _firstTouchPosition.x) * 180 / Mathf.PI;
                MovePieces();
                _board._currentCat = this;
            }
            else
            {
                _board._currentState = GameState.move;
            }

        }

        void MovePiecesActual(Vector2 direction)
        {
            _otherCat = _board._allCats[_column + (int)direction.x, _row + (int)direction.y];
            _previousRow = _row;
            _previousColumn = _column;
            if (_board._lockTiles[_column, _row] == null && _board._lockTiles[_column + (int)direction.x, _row + (int)direction.y] == null)
            {
                if (_otherCat != null)
                {
                    _otherCat.GetComponent<Cat>()._column += -1 * (int)direction.x;
                    _otherCat.GetComponent<Cat>()._row += -1 * (int)direction.y;
                    _column += (int)direction.x;
                    _row += (int)direction.y;
                    StartCoroutine(CheckMoveCoroutine());
                }
                else
                {
                    _board._currentState = GameState.move;
                }
            }
            else
            {
                _board._currentState = GameState.move;
            }
        }

        void MovePieces()
        {
            if (swipeAngle > -45 && swipeAngle <= 45 && _column < _board._width - 1)
            {
                MovePiecesActual(Vector2.right);
            }
            else if (swipeAngle > 45 && swipeAngle <= 135 && _row < _board._height - 1)
            {
                MovePiecesActual(Vector2.up);
            }
            else if ((swipeAngle > 135 || swipeAngle <= -135) && _column > 0)
            {
                MovePiecesActual(Vector2.left);
            }
            else if (swipeAngle < -45 && swipeAngle >= -135 && _row > 0)
            {
                MovePiecesActual(Vector2.down);
            }
            else
            {
                _board._currentState = GameState.move;
            }


        }

        void FindMatches()
        {
            if (_column > 0 && _column < _board._width - 1)
            {
                GameObject leftCat1 = _board._allCats[_column - 1, _row];
                GameObject rightCat1 = _board._allCats[_column + 1, _row];
                if (leftCat1 != null && rightCat1 != null)
                {
                    if (leftCat1.tag == this.gameObject.tag && rightCat1.tag == this.gameObject.tag)
                    {
                        leftCat1.GetComponent<Cat>().isMatched = true;
                        rightCat1.GetComponent<Cat>().isMatched = true;
                        isMatched = true;
                    }
                }

            }
            if (_row > 0 && _row < _board._height - 1)
            {
                GameObject upCat1 = _board._allCats[_column, _row + 1];
                GameObject downCat1 = _board._allCats[_column, _row - 1];
                if (upCat1 != null && downCat1 != null)
                {
                    if (upCat1.tag == this.gameObject.tag && downCat1.tag == this.gameObject.tag)
                    {
                        upCat1.GetComponent<Cat>().isMatched = true;
                        downCat1.GetComponent<Cat>().isMatched = true;
                        isMatched = true;
                    }
                }

            }
        }

        public void MakeRowBomb()
        {
            if (!_isColumnBomb && !_isColorBomb && !_isExplBomb)
            {
                _isRowBomb = true;
                GameObject arrow = Instantiate(_rowArrow, transform.position, Quaternion.identity);
                arrow.transform.parent = this.transform;
            }
        }

        public void MakeColumnBomb()
        {
            if (!_isRowBomb && !_isColorBomb && !_isExplBomb)
            {
                _isColumnBomb = true;
                GameObject arrow = Instantiate(_columnArrow, transform.position, Quaternion.identity);
                arrow.transform.parent = this.transform;
            }
        }

        public void MakeColorBomb()
        {
            if (!_isColumnBomb && !_isRowBomb && !_isExplBomb)
            {
                _isColorBomb = true;
                GameObject color = Instantiate(_colorBomb, transform.position, Quaternion.identity);
                color.transform.parent = this.transform;
                this.gameObject.tag = "Color";
            }
        }

        public void MakeExplBomb()
        {
            if (!_isColumnBomb && !_isColorBomb && !_isRowBomb)
            {
                _isExplBomb = true;
                GameObject marker = Instantiate(_explBomb, transform.position, Quaternion.identity);
                marker.transform.parent = this.transform;
            }
        }
    }
}
