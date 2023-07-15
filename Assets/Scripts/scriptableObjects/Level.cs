using Cats3.Base;
using Cats3.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "World", menuName = "Level")]
    public class Level : ScriptableObject
    {
        [Header("������� �����")]
        public int _width;
        public int _height;

        [Header("��������� ������")]
        public TileType[] _boardLayout;

        [Header("��������� �����")]
        public GameObject[] _cats;

        [Header("����")]
        public int[] _scoreGoals;

        [Header("���� ����")]
        public EndGameRequirements _endRequirements;
        public BlankGoal[] _levelGoals;
    }
}
