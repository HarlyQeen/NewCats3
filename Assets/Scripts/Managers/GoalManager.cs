using Cats3.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Managers
{
    [System.Serializable]
    public class BlankGoal
    {
        public int _numberNeeded;
        public int _numberColected;
        public Sprite _goalSprite;
        public string matchValue;
    }
    public class GoalManager : MonoBehaviour
    {
        public BlankGoal[] _levelGoals;
        public List<GoalPanel> currentsGoals = new List<GoalPanel>();
        public GameObject _goalPrefab;
        public GameObject _goalIntroParent;
        public GameObject _goalGameParent;
        private Board _board;
        private EndGameManager _endGame;

        void Start()
        {
            _board = FindObjectOfType<Board>();
            _endGame = FindObjectOfType<EndGameManager>();
            GetGoals();
            SetupGoals();
        }

        void GetGoals()
        {
            if(_board != null)
            {
                if(_board._world != null)
                {
                    if (_board._level < _board._world._levels.Length)
                    {
                        if (_board._world._levels[_board._level] != null)
                        {
                            _levelGoals = _board._world._levels[_board._level]._levelGoals;
                            for(int i = 0; i < _levelGoals.Length; i++)
                            {
                                _levelGoals[i]._numberColected = 0;
                            }
                        }
                    }
                }
            }
        }

        void SetupGoals()
        {
            for (int i = 0; i < _levelGoals.Length; i++)
            {
                //������� ����� GoalPanelPrefab  � ��������������� �������
                GameObject goal = Instantiate(_goalPrefab, _goalIntroParent.transform.position, Quaternion.identity);
                goal.transform.SetParent(_goalIntroParent.transform, false);
                //������� �������� � ����� ��� �����
                GoalPanel panel = goal.GetComponent<GoalPanel>();
                panel._thisSprite = _levelGoals[i]._goalSprite;
                panel._thisString = "0/" + _levelGoals[i]._numberNeeded;

                //������� ����� GoalPanelPrefab  � �������������� �������
                GameObject gameGoal = Instantiate(_goalPrefab, _goalGameParent.transform.position, Quaternion.identity);
                gameGoal.transform.SetParent(_goalGameParent.transform, false);
                panel = gameGoal.GetComponent<GoalPanel>();
                currentsGoals.Add(panel); //������� ���
                panel._thisSprite = _levelGoals[i]._goalSprite;
                panel._thisString = "0/" + _levelGoals[i]._numberNeeded;
            }
        }

        public void UpdateGoals()
        {
            int _goalsCompleted = 0;
            for (int i = 0; i < _levelGoals.Length; i++)
            {
                currentsGoals[i]._thisText.text = "" + _levelGoals[i]._numberColected + "/" + _levelGoals[i]._numberNeeded;
                if (_levelGoals[i]._numberColected >= _levelGoals[i]._numberNeeded)
                {
                    _goalsCompleted++;
                    currentsGoals[i]._thisText.text = "" + _levelGoals[i]._numberNeeded + "/" + _levelGoals[i]._numberNeeded;
                }
                if (_goalsCompleted >= _levelGoals.Length)
                {
                    if (_endGame != null)
                    {
                        _endGame.WinGame();
                    }
                    Debug.Log("�� �������!!!");
                }
            }
        }

        public void CompareGoal(string goalToCompare) //������� ����
        {
            for (int i = 0; i < _levelGoals.Length; i++)
            {
                if (goalToCompare == _levelGoals[i].matchValue)
                {
                    _levelGoals[i]._numberColected++;
                }
            }
        }
    }
}
