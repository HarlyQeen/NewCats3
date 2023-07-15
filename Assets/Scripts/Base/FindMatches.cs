using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cats3.Base
{
    public class FindMatches : MonoBehaviour
    {
        private Board _board;
        public List<GameObject> currentMatches = new List<GameObject>();

        void Start()
        {
            _board = FindObjectOfType<Board>();
        }

        public void FindAllMatches()
        {
            StartCoroutine(FindAllMatchesCoroutine());
        }

        private List<GameObject> IsExplBomb(Cat cat1, Cat cat2, Cat cat3)
        {
            List<GameObject> currentCats = new List<GameObject>();
            if (cat1._isExplBomb)
            {
                currentMatches.Union(GetExplBombPieces(cat1._column, cat1._row));
            }

            if (cat2._isExplBomb)
            {
                currentMatches.Union(GetExplBombPieces(cat2._column, cat2._row));
            }

            if (cat3._isExplBomb)
            {
                currentMatches.Union(GetExplBombPieces(cat3._column, cat3._row));
            }
            return currentCats;
        }

        private List<GameObject> IsRowBomb(Cat cat1, Cat cat2, Cat cat3)
        {
            List<GameObject> currentCats = new List<GameObject>();
            if (cat1._isRowBomb)
            {
                currentMatches.Union(GetRowPieces(cat1._row));
            }

            if (cat2._isRowBomb)
            {
                currentMatches.Union(GetRowPieces(cat2._row));
            }

            if (cat3._isRowBomb)
            {
                currentMatches.Union(GetRowPieces(cat3._row));
            }
            return currentCats;
        }

        private List<GameObject> IsColumnBomb(Cat cat1, Cat cat2, Cat cat3)
        {
            List<GameObject> currentCats = new List<GameObject>();
            if (cat1._isColumnBomb)
            {
                currentMatches.Union(GetColumnPieces(cat1._column));
            }

            if (cat2._isColumnBomb)
            {
                currentMatches.Union(GetColumnPieces(cat2._column));
            }

            if (cat3._isColumnBomb)
            {
                currentMatches.Union(GetColumnPieces(cat3._column));
            }
            return currentCats;
        }

        private void AddToListAndMatch(GameObject cat)
        {
            if (!currentMatches.Contains(cat))
            {
                currentMatches.Add(cat);
            }
            cat.GetComponent<Cat>().isMatched = true;
        }

        private void GetNearbyPieces(GameObject cat1, GameObject cat2, GameObject cat3)
        {
            AddToListAndMatch(cat1);
            AddToListAndMatch(cat2);
            AddToListAndMatch(cat3);
        }

        private IEnumerator FindAllMatchesCoroutine()
        {
            yield return new WaitForSeconds(.2f);
            //yield return null;
            for (int i = 0; i < _board._width; i++)
            {
                for (int j = 0; j < _board._height; j++)
                {
                    GameObject currentCat = _board._allCats[i, j];

                    if (currentCat != null)
                    {
                        Cat currentCatCat = currentCat.GetComponent<Cat>();
                        if (i > 0 && i < _board._width - 1)
                        {
                            GameObject leftCat = _board._allCats[i - 1, j];

                            GameObject rightCat = _board._allCats[i + 1, j];

                            if (leftCat != null && rightCat != null)
                            {
                                Cat rightCatCat = rightCat.GetComponent<Cat>();
                                Cat leftCatCat = leftCat.GetComponent<Cat>();
                                if (leftCat.tag == currentCat.tag && rightCat.tag == currentCat.tag)
                                {

                                    currentMatches.Union(IsRowBomb(leftCatCat, currentCatCat, rightCatCat));

                                    currentMatches.Union(IsColumnBomb(leftCatCat, currentCatCat, rightCatCat));

                                    currentMatches.Union(IsExplBomb(leftCatCat, currentCatCat, rightCatCat));

                                    GetNearbyPieces(leftCat, currentCat, rightCat);

                                }
                                /*if (leftCat != null && rightCat != null)
                                {

                                }*/
                            }

                        }

                        if (j > 0 && j < _board._height - 1)
                        {
                            GameObject upCat = _board._allCats[i, j + 1];

                            GameObject downCat = _board._allCats[i, j - 1];
                            if (upCat != null && downCat != null)
                            {
                                Cat downCatCat = downCat.GetComponent<Cat>();
                                Cat upCatCat = upCat.GetComponent<Cat>();
                                if (upCat != null && downCat != null)
                                {
                                    if (upCat.tag == currentCat.tag && downCat.tag == currentCat.tag)
                                    {
                                        currentMatches.Union(IsColumnBomb(upCatCat, currentCatCat, downCatCat));

                                        currentMatches.Union(IsRowBomb(upCatCat, currentCatCat, downCatCat));

                                        currentMatches.Union(IsExplBomb(upCatCat, currentCatCat, downCatCat));

                                        GetNearbyPieces(upCat, currentCat, downCat);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            yield return null;
        }
        

        public void MatchPiecesOfColor(string color)
        {
            for (int i = 0; i < _board._width; i++)
            {
                for (int j = 0; j < _board._height; j++)
                {
                    //Проверим, существует ли эта часть
                    if (_board._allCats[i, j] != null)
                    {
                        //Проверим тэги на кошках
                        if (_board._allCats[i, j].tag == color)
                        {
                            //Установим соответствие кошек
                            _board._allCats[i, j].GetComponent<Cat>().isMatched = true;
                        }
                    }
                }
            }
        }

        List<GameObject> GetExplBombPieces(int _column, int _row)
        {
            List<GameObject> _cats = new List<GameObject>();
            for (int i = _column - 1; i <= _column + 1; i++)
            {
                for (int j = _row - 1; j <= _row + 1; j++)
                {
                    //Проверим, находится ли деталь внутри доски
                    if (i >= 0 && i < _board._width && j >= 0 && j < _board._height)
                    {
                        if (_board._allCats[i, j] != null)
                        {
                            _cats.Add(_board._allCats[i, j]);
                            _board._allCats[i, j].GetComponent<Cat>().isMatched = true;
                        }

                    }
                }
            }
            return _cats;
        }

        List<GameObject> GetColumnPieces(int _column)
        {
            List<GameObject> _cats = new List<GameObject>();
            for (int i = 0; i < _board._height; i++)
            {
                if (_board._allCats[_column, i] != null)
                {
                    Cat cat = _board._allCats[_column, i].GetComponent<Cat>();
                    if (cat._isRowBomb)
                    {
                        _cats.Union(GetRowPieces(i)).ToList();
                    }
                    _cats.Add(_board._allCats[_column, i]);
                    cat.isMatched = true;
                }
            }
            return _cats;
        }

        List<GameObject> GetRowPieces(int _row)
        {
            List<GameObject> _cats = new List<GameObject>();
            for (int i = 0; i < _board._width; i++)
            {
                if (_board._allCats[i, _row] != null)
                {
                    Cat cat = _board._allCats[i, _row].GetComponent<Cat>();
                    if (cat._isColumnBomb)
                    {
                        _cats.Union(GetColumnPieces(i)).ToList();
                    }
                    _cats.Add(_board._allCats[i, _row]);
                    cat.isMatched = true;
                }
            }
            return _cats;
        }

        public void CheckBombs()
        {
            //Должен ли игрок что-то двигать?
            if (_board._currentCat != null)
            {
                //Совпадает ли часть, которую переместили?
                if (_board._currentCat.isMatched)
                {
                    //Сделать их несовпадающими
                    _board._currentCat.isMatched = false;
                    //понять, какую бомбу сделать
                    /*int typeOfBomb = Random.Range(0, 100);
                    if(typeOfBomb < 50)
                    {
                        //В ряд
                        _board._currentCat.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        //В колонку
                        _board._currentCat.MakeColumnBomb();
                    }*/

                    if ((_board._currentCat.swipeAngle > -45 && _board._currentCat.swipeAngle <= 45)
                         || (_board._currentCat.swipeAngle < -135 && _board._currentCat.swipeAngle >= 135))
                    {
                        _board._currentCat.MakeRowBomb();
                    }
                    else
                    {
                        _board._currentCat.MakeColumnBomb();
                    }

                }
                //Совпадает ли другая часть?
                else if (_board._currentCat._otherCat != null)
                {
                    Cat _otherCat = _board._currentCat._otherCat.GetComponent<Cat>();
                    //Другие коты одинаковые?
                    if (_otherCat.isMatched)
                    {
                        //Сделаем их разными
                        _otherCat.isMatched = false;

                        //выбираем вид бомбы
                        /* int typeOfBomb = Random.Range(0, 100);
                         if (typeOfBomb < 50)
                         {
                             //В ряд
                             _otherCat.MakeRowBomb();
                         }
                         else if (typeOfBomb >= 50)
                         {
                             //В колонку
                             _otherCat.MakeColumnBomb();
                         }*/

                        if ((_board._currentCat.swipeAngle > -45 && _board._currentCat.swipeAngle <= 45)
                         || (_board._currentCat.swipeAngle < -135 && _board._currentCat.swipeAngle >= 135))
                        {
                            _otherCat.MakeRowBomb();
                        }
                        else
                        {
                            _otherCat.MakeColumnBomb();
                        }
                    }
                }
            }
        }
    }
}
