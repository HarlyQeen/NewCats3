using Cats3.Managers;
using Cats3.ScriptableObjects;
using Cats3.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Base
{
    public enum GameState
    {
        wait,
        move,
        win,
        lose,
        pause
    }

    public enum TileKind
    {
        Breakable,
        Blank,
        Lock,
        Normal
    }




    [System.Serializable]
    public class TileType
    {
        public int x;
        public int y;
        public TileKind _tileKind;
    }

    public class Board : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        public World _world;
        public int _level;

        public GameState _currentState = GameState.move;

        [Header("Размеры доски")]
        public int _width;
        public int _height;
        public int _offSet;

        [Header("Префабы")]
        public GameObject _tilePrefab;
        public GameObject _breakableTilePrefab;
        public GameObject _lockPrefab;
        public GameObject[] _cats;
        public GameObject _destroyEffect;

        [Header("План стола")]
        public TileType[] _boardLayout;
        private bool[,] _blankSpaces;
        private BackgroundTile[,] _breakableTiles;
        public BackgroundTile[,] _lockTiles;
        public GameObject[,] _allCats;

        [Header("Совпадения")]
        public Cat _currentCat;
        private FindMatches _findMatches;
        public int basePieceValue = 20;
        private int streakValue = 1;
        private ScoreManager _scoreManager;
        private SoundManager _soundManager;
        private GoalManager _goalManager;
        private PauseMenu _pauseManager;
        public float _refillDelay = 0.5f;
        public int[] _scoreGoals;

        private void Awake()
        {
            if(PlayerPrefs.HasKey("Выбор уровня"))
            {
                _level = PlayerPrefs.GetInt("Выбор уровня");
            }    
            if(_world != null)
            {
                if (_level < _world._levels.Length)
                {
                    if (_world._levels[_level] != null)
                    {
                        _width = _world._levels[_level]._width;
                        _height = _world._levels[_level]._height;
                        _cats = _world._levels[_level]._cats;
                        _scoreGoals = _world._levels[_level]._scoreGoals;
                        _boardLayout = _world._levels[_level]._boardLayout;
                    }
                }
            }
        }

        void Start()
        {
            _pauseManager = FindObjectOfType<PauseMenu>();
            _goalManager = FindObjectOfType<GoalManager>();
            _soundManager = FindObjectOfType<SoundManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            _breakableTiles = new BackgroundTile[_width, _height];
            _lockTiles = new BackgroundTile[_width, _height];
            _findMatches = FindObjectOfType<FindMatches>();
            _blankSpaces = new bool[_width, _height];
            _allCats = new GameObject[_width, _height];
            SetUp();
            _currentState = GameState.pause;
        }

        public void GenerateBlankSpaces()
        {
            //Посмотрим на все плитки
            for (int i = 0; i < _boardLayout.Length; i++)
            {
                //Если плитка - кирпичная
                if (_boardLayout[i]._tileKind == TileKind.Blank)
                {
                    //Сделаем плитку кирпичной
                    _blankSpaces[_boardLayout[i].x, _boardLayout[i].y] = true;
                }
            }
        }

        public void GenerateBreakableTiles()
        {
            for (int i = 0; i < _boardLayout.Length; i++)
            {
                if (_boardLayout[i]._tileKind == TileKind.Breakable)
                {
                    //Сделаем плитку кирпичной
                    Vector2 tempPosition = new Vector2(_boardLayout[i].x, _boardLayout[i].y);
                    GameObject tile = Instantiate(_breakableTilePrefab, tempPosition, Quaternion.identity);
                    _breakableTiles[_boardLayout[i].x, _boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
                }
            }
        }

        private void GenerateLockTiles()
        {
            for (int i = 0; i < _boardLayout.Length; i++)
            {
                //if a tile is a "Lock" tile
                if (_boardLayout[i]._tileKind == TileKind.Lock)
                {
                    //Create a "Lock" tile at that position;
                    Vector2 tempPosition = new Vector2(_boardLayout[i].x, _boardLayout[i].y);
                    GameObject tile = Instantiate(_lockPrefab, tempPosition, Quaternion.identity);
                    _lockTiles[_boardLayout[i].x, _boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
                }
            }
        }
        private void SetUp()
        {
            GenerateBlankSpaces();
            GenerateBreakableTiles();
            GenerateLockTiles();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (!_blankSpaces[i, j])
                    {
                        Vector2 tempPosition = new Vector2(i, j + _offSet);
                        Vector2 tilePosition = new Vector2(i, j);
                        GameObject backgroundTile = Instantiate(_tilePrefab, tilePosition, Quaternion.identity) as GameObject;
                        backgroundTile.transform.parent = this.transform;
                        backgroundTile.name = "(" + i + "," + j + ")";
                        int catToUse = Random.Range(0, _cats.Length);

                        int maxIterations = 0;
                        while (MatchesAt(i, j, _cats[catToUse]) && maxIterations < 100)
                        {
                            catToUse = Random.Range(0, _cats.Length);
                            maxIterations++;
                            Debug.Log(maxIterations);
                        }
                        maxIterations = 0;

                        GameObject cat = Instantiate(_cats[catToUse], tempPosition, Quaternion.identity);
                        cat.GetComponent<Cat>()._row = j;
                        cat.GetComponent<Cat>()._column = i;

                        cat.transform.parent = this.transform;
                        cat.name = "(" + i + "," + j + ")";
                        _allCats[i, j] = cat;
                    }
                }
            }
        }

        private bool MatchesAt(int _column, int _row, GameObject piece)
        {
            if (_column > 1 && _row > 1)
            {
                if (_allCats[_column - 1, _row] != null && _allCats[_column - 2, _row] != null)
                {
                    if (_allCats[_column - 1, _row].tag == piece.tag && _allCats[_column - 2, _row].tag == piece.tag)
                    {
                        return true;
                    }
                }
                if (_allCats[_column, _row - 1] != null && _allCats[_column, _row - 2] != null)
                {
                    if (_allCats[_column, _row - 1].tag == piece.tag && _allCats[_column, _row - 2].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
            else if (_column <= 1 || _row <= 1)
            {
                if (_row > 1)
                {
                    if (_allCats[_column, _row - 1] != null && _allCats[_column, _row - 2] != null)
                    {
                        if (_allCats[_column, _row - 1].tag == piece.tag && _allCats[_column, _row - 2].tag == piece.tag)
                        {
                            return true;
                        }
                    }

                }
                if (_column > 1)
                {
                    if (_allCats[_column - 1, _row] != null && _allCats[_column - 2, _row] != null)
                    {
                        if (_allCats[_column - 1, _row].tag == piece.tag && _allCats[_column - 2, _row].tag == piece.tag)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private int ColumnOrRow() //был bool, потом int
        {

            //Сделаем копию совпадений
            List<GameObject> _matchCopy = _findMatches.currentMatches as List<GameObject>;
            
            
            //Просмотреть копии совпадений и решить - делать ли бомбу
            for(int i = 0; i < _matchCopy.Count; i++)
            {
                //Сохраняем эту киску
                Cat thisCat = _matchCopy[i].GetComponent<Cat>();
                int _column = thisCat._column;
                int _row = thisCat._row;
                int columnMatch = 0;
                int rowMatch = 0;
                //Посмотреть на другие части и сравнить
                for(int j = 0; j < _matchCopy.Count; j++)
                {
                    //Сохраним следующую киску
                    Cat nextCat = _matchCopy[j].GetComponent<Cat>();
                    if(nextCat == thisCat)
                    {
                        continue;
                    }
                    if(nextCat._column == thisCat._column && nextCat.CompareTag(thisCat.tag))
                    {
                        columnMatch++;
                    }
                    if (nextCat._row == thisCat._row && nextCat.CompareTag(thisCat.tag))
                    {
                        rowMatch++;
                    }
                }
                //верну 1 - цветная бомба
                //верну 2 - если взрыв
                //верну 3 - если уничтожение ряда/колонны
                if(columnMatch == 4 || rowMatch == 4)
                {
                    return 1;
                }
                if (columnMatch == 2 || rowMatch == 2)
                {
                    return 2;
                }
                if (columnMatch == 3 || rowMatch == 3)
                {
                    return 3;
                }
            }

            return 0;
        }

        private void CheckToMakeBombs()
        {

            //Сколько объектов находится в поиске совпадений в текущих совпадениях
            if(_findMatches.currentMatches.Count > 3)
            {
                //какой тип совпадений?
                int typeOfMatch = ColumnOrRow();
                if (typeOfMatch == 1)
                {
                    //Сделаю цветную бомбу
                    //Совпадает ли текущие коты?
                    if (_currentCat != null)
                    {
                        if (_currentCat.isMatched)
                        {
                            if(!_currentCat._isColorBomb)
                            {
                                _currentCat.isMatched = false;
                                _currentCat.MakeColorBomb();
                            }
                            
                        }
                    }
                    else
                    {
                        if (_currentCat._otherCat != null)
                        {
                            Cat _otherCat = _currentCat._otherCat.GetComponent<Cat>();
                            if (_otherCat.isMatched)
                            {
                                if (!_otherCat._isColorBomb)
                                {
                                    _otherCat.isMatched = false;
                                    _otherCat.MakeColorBomb();
                                }

                            }
                        }
                    } 
                }
                else if (typeOfMatch == 2)
                {
                    //сделаю взрыв
                    if (_currentCat != null)
                    {
                        if(_currentCat.isMatched)
                        {
                            if(!_currentCat._isExplBomb)
                            {
                                _currentCat.isMatched = false;
                                _currentCat.MakeExplBomb();
                            }
                        }
                    
                    }
                    else
                    {
                        if (_currentCat._otherCat != null)
                        {
                            Cat _otherCat = _currentCat._otherCat.GetComponent<Cat>();
                            if (_otherCat.isMatched)
                            {
                                if(!_otherCat._isExplBomb)
                                {
                                    _otherCat.isMatched = false;
                                    _otherCat.MakeExplBomb();
                                }
                            }
                        }
                    }
                }
                else if(typeOfMatch == 3)
                {
                    _findMatches.CheckBombs();
                }
            }
        }

        private void DestroyMatchesAt(int _column, int _row)
        {
            if (_allCats[_column, _row].GetComponent<Cat>().isMatched)
            {
                //Надо ли сломать плитку?
                if (_breakableTiles[_column, _row] != null)
                {
                    //Нанесем 1 урон
                    _breakableTiles[_column, _row].TakeDamage(1);
                    if (_breakableTiles[_column, _row]._hitPoints <= 0)
                    {
                        _breakableTiles[_column, _row] = null;
                    }
                }
                //Надо ли сломать цепи?
                if (_lockTiles[_column, _row] != null)
                {
                    _lockTiles[_column, _row].TakeDamage(1);
                    if (_lockTiles[_column, _row]._hitPoints <= 0)
                    {
                        _lockTiles[_column, _row] = null;
                    }
                }
                if (_goalManager != null)
                {
                    _goalManager.CompareGoal(_allCats[_column, _row].tag.ToString());
                    _goalManager.UpdateGoals();
                }

                //Музык. мен. 
                if (_soundManager != null)
                {
                    _soundManager.PlayRandomDestroyCats();
                }
                //_findMatches.currentMatches.Remove(_allCats[_column, _row]);
                GameObject particle = Instantiate(_destroyEffect, _allCats[_column, _row].transform.position, Quaternion.identity);
                Destroy(particle, .5f);
                Destroy(_allCats[_column, _row]);
                _scoreManager.CounterScore(basePieceValue * streakValue);
                _allCats[_column, _row] = null;
            }
        }

        public void DestroyMatches()
        {
            //Сколько элементов в списке совпадаемых частей "поиска одинаковых"
            if (_findMatches.currentMatches.Count >= 4)
            {
                CheckToMakeBombs();
            }
            _findMatches.currentMatches.Clear();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] != null)
                    {
                        DestroyMatchesAt(i, j);
                    }
                }
            }
            
            Debug.Log("Проверка уменьшения строк");
            StartCoroutine(DecreaseRowCoroutine2());
        }

        private IEnumerator DecreaseRowCoroutine2()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    //бланк. Если текущее место - не бланк и пустой
                    if (!_blankSpaces[i, j] && _allCats[i, j] == null)
                    {
                        //петля от верха до верхней части колонки
                        for (int k = j + 1; k < _height; k++)
                        {
                            //если кот найден
                            if (_allCats[i, k] != null)
                            {
                                //перемещаем его в это пустое пространство
                                _allCats[i, k].GetComponent<Cat>()._row = j;
                                //установим это место нулевым
                                _allCats[i, k] = null;
                                //Разрываем петлю
                                break;
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(_refillDelay * 0.5f);
            StartCoroutine(FillBoardCoroutine());
        }

        private IEnumerator DecreaseRowCoroutine()
        {
            int nullCount = 0;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] == null)
                    {
                        nullCount++;
                    }
                    else if (nullCount > 0)
                    {
                        _allCats[i, j].GetComponent<Cat>()._row -= nullCount;
                        _allCats[i, j] = null;
                    }
                }
                nullCount = 0;
            }
            yield return new WaitForSeconds(.4f);
            StartCoroutine(FillBoardCoroutine());
        }

        private void RefillBoard()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] == null && !_blankSpaces[i, j])
                    {
                        Vector2 tempPosition = new Vector2(i, j + _offSet);
                        int catToUse = Random.Range(0, _cats.Length);
                        int maxIterations = 0;
                        while (MatchesAt(i, j, _cats[catToUse]) && maxIterations < 100)
                        {
                            maxIterations++;
                            catToUse = Random.Range(0, _cats.Length);
                        }
                        maxIterations = 0;
                        GameObject piece = Instantiate(_cats[catToUse], tempPosition, Quaternion.identity);
                        _allCats[i, j] = piece;
                        piece.GetComponent<Cat>()._row = j;
                        piece.GetComponent<Cat>()._column = i;
                    }
                }
            }
        }

        private bool MatchesOnBoard()
        {
            _findMatches.FindAllMatches();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] != null)
                    {
                        if (_allCats[i, j].GetComponent<Cat>().isMatched)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private IEnumerator FillBoardCoroutine()
        {
            yield return new WaitForSeconds(_refillDelay);
            RefillBoard();
            yield return new WaitForSeconds(_refillDelay);
            while (MatchesOnBoard())
            {
                streakValue++;
                DestroyMatches();
                yield break;

            }
            _currentCat = null;

            if (IsDeadLocked())
            {
                StartCoroutine(ShuffleBoard());
            }
            yield return new WaitForSeconds(_refillDelay);
            Debug.Log("Дополнено");
            if(_currentState != GameState.pause)
            _currentState = GameState.move;

            streakValue = 1;
        }

        private void SwitchPieces(int _column, int _row, Vector2 direction)
        {
            if (_allCats[_column + (int)direction.x, _row + (int)direction.y] != null)
            {
                //Возьмем 2 сборку и сохраним
                GameObject holder = _allCats[_column + (int)direction.x, _row + (int)direction.y] as GameObject;
                //переключение первой кошки во 2 позицию
                _allCats[_column + (int)direction.x, _row + (int)direction.y] = _allCats[_column, _row];
                //Установим 1 кошку на место 2 кошки
                _allCats[_column, _row] = holder;
            }
        }

        private bool CheckForMatces()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] != null)
                    {
                        //убедимся, что одна и 2 справа на столе
                        if (i < _width - 2)
                        {
                            //проверим, есть ли кошка или 2 справа
                            if (_allCats[i + 1, j] != null && _allCats[i + 2, j] != null)
                            {
                                if (_allCats[i + 1, j].tag == _allCats[i, j].tag
                                    && _allCats[i + 2, j].tag == _allCats[i, j].tag)
                                {
                                    return true;
                                }
                            }
                        }

                        if (j < _height - 2)
                        {
                            //Проверим, существуют ли вышеперечисленные кошки
                            if (_allCats[i, j + 1] != null && _allCats[i, j + 2] != null)
                            {
                                if (_allCats[i, j + 1].tag == _allCats[i, j].tag
                                    && _allCats[i, j + 2].tag == _allCats[i, j].tag)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool SwitchAndCheck(int _column, int _row, Vector2 direction)
        {
            SwitchPieces(_column, _row, direction);
            if (CheckForMatces())
            {
                SwitchPieces(_column, _row, direction);
                return true;
            }
            SwitchPieces(_column, _row, direction);
            return false;
        }

        public bool IsDeadLocked()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] != null)
                    {
                        if (i < _width - 1)
                        {
                            if (SwitchAndCheck(i, j, Vector2.right))
                            {
                                return false;
                            }
                        }

                        if (j < _height - 1)
                        {
                            if (SwitchAndCheck(i, j, Vector2.up))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private IEnumerator ShuffleBoard()
        {
            yield return new WaitForSeconds(0.5f);
            //Составлю список геймобджектов
            List<GameObject> newBoard = new List<GameObject>();
            //Добавлю каждый кусочек в него
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_allCats[i, j] != null)
                    {
                        newBoard.Add(_allCats[i, j]);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
            //Для каждого места на столе
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    //если этоместо не blank
                    if (!_blankSpaces[i, j])
                    {
                        //Выберем рондомное число
                        int pieceToUse = Random.Range(0, newBoard.Count);

                        //Назначим колонку мест
                        int maxIterations = 0;
                        while (MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations < 100)
                        {
                            pieceToUse = Random.Range(0, newBoard.Count);
                            maxIterations++;
                            Debug.Log(maxIterations);
                        }
                        //Сделаем контейнер для мест
                        Cat piece = newBoard[pieceToUse].GetComponent<Cat>();
                        maxIterations = 0;
                        piece._column = i;
                        //Назначим ряд мест
                        piece._row = j;
                        //Заполним массив кошек этой новой частью
                        _allCats[i, j] = newBoard[pieceToUse];
                        //Удалим это
                        newBoard.Remove(newBoard[pieceToUse]);
                    }
                }
            }


            //Порверка на deadlock
            if (IsDeadLocked())
            {
                StartCoroutine(ShuffleBoard());
            }
        }
    }
}
