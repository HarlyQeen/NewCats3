using Cats3.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Base
{
    public class BackgroundTile : MonoBehaviour
    {
        public int _hitPoints;
        private SpriteRenderer _sprite;
        private GoalManager _goalManager;

        private void Start()
        {
            _goalManager = FindObjectOfType<GoalManager>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_hitPoints <= 0)
            {
                if (_goalManager != null)
                {
                    _goalManager.CompareGoal(this.gameObject.tag);
                    _goalManager.UpdateGoals();
                }
                Destroy(this.gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            MakeLighter();
        }

        void MakeLighter()
        {
            //Выберем нужный цвет
            Color color = _sprite.color;
            //Сделать нужный альфа
            float newAlpha = color.a * .5f;
            _sprite.color = new Color(color.r, color.g, color.b, newAlpha);
        }
    }
}
