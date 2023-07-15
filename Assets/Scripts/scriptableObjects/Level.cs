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
        [Header("Размеры доски")]
        public int _width;
        public int _height;

        [Header("Стартовые клетки")]
        public TileType[] _boardLayout;

        [Header("Доступные киски")]
        public GameObject[] _cats;

        [Header("Очки")]
        public int[] _scoreGoals;

        [Header("Цели игры")]
        public EndGameRequirements _endRequirements;
        public BlankGoal[] _levelGoals;
    }
}
