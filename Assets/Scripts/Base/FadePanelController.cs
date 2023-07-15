using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Base
{
    public class FadePanelController : MonoBehaviour
    {
        public Animator _fadeAnimator;
        public Animator _gameAnimator;

        public void OK_Button()
        {
            if (_fadeAnimator != null && _gameAnimator != null)
            {
                _fadeAnimator.SetBool("Out", true);
                _gameAnimator.SetBool("Out", true);
                StartCoroutine(StartGameCoroutine());
            }
        }

        public void GameOver()
        {
            _fadeAnimator.SetBool("Out", false);
            _fadeAnimator.SetBool("Game Over", true);
        }

        IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(1f);
            Board _board = FindObjectOfType<Board>();
            _board._currentState = GameState.move;
        }

    }
}
