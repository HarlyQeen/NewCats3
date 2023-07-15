using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Base
{

    public class CatMove : MonoBehaviour
    {
        [SerializeField]
        public float _speed = 1f;
        private float _waitTime;
        public float _startWait;
        public Transform[] _moveSpots;
        private int _randomeSpot;
        private Rigidbody2D _rb;
        private bool _faceToLeft = true;
        public Transform _groundDetection;
        public float _distance;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _waitTime = _startWait;
            _randomeSpot = Random.Range(0, _moveSpots.Length);
        }


        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_randomeSpot].position, _speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, _moveSpots[_randomeSpot].position) < .2f)
            {
                if (_waitTime <= 0)
                {
                    
                    _randomeSpot = Random.Range(0, _moveSpots.Length);
                    _waitTime = _startWait;
                    _animator.SetBool("IsWalk", true);
                }
                else
                {
                    
                    _waitTime -= Time.deltaTime;
                    _animator.SetBool("IsWalk", false);

                }
            }

            RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance);
            if (groundInfo.collider == false)
            {
                if (_faceToLeft == true)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    _faceToLeft = false;
                }
                else 
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    _faceToLeft = true;
                }
            }
        }
    }
}
