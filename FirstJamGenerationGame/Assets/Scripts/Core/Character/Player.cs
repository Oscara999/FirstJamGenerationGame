using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core.Character
{

    public class Player : MonoBehaviour
    {
        public static Action<bool> OnPreviousReached;
        private bool _rewind = false;
        private Vector3 _prevPosition;
        private float _speed;
        private Rigidbody _rigidBody;
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        public void SetRewind(Vector3 prevPosition, float speed)
        {
            _rewind = true;
            _prevPosition = prevPosition;
            _speed = speed;
            _rigidBody.useGravity = false;
            OnPreviousReached?.Invoke(false);

        }

        private void MoveToPrevious() => 
            transform.position = Vector3.MoveTowards(transform.position, _prevPosition, _speed * Time.deltaTime);
        void Update()
        {
            if (_rewind)
            {
                MoveToPrevious();
                if (Vector3.Distance(transform.position, _prevPosition) <= 0)
                {
                    _rewind = false;
                    _rigidBody.useGravity = true;
                    OnPreviousReached?.Invoke(!_rewind);
                }
            }

        }
    }


}