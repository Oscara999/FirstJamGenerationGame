using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Platforms
{
    public class RotativePlatform : Platform
    {
        [SerializeField] float _turnSpeed = 5.0f;
        [SerializeField] float[] _randomRotationTimes= {2.4f, 3.5f};
        private bool _canRotate = true;
        private float _timeToRotate;
        protected override void Behavior()
        {

            if (_canRotate)
            {
                StartCoroutine(TimeToRotate());
               
            }
            if (Vector3.Dot(transform.up, Vector3.up) == 1 || Vector3.Dot(transform.up, Vector3.up) == -1)
            {
                StopCoroutine(TimeToRotate());
            }
        }

        IEnumerator TimeToRotate()
        {
            _canRotate = false;
            _timeToRotate = UnityEngine.Random.Range(_randomRotationTimes[0], _randomRotationTimes[1]);
            while(_timeToRotate > 0)
            {
                transform.Rotate(Vector3.right, _turnSpeed * Time.deltaTime, Space.Self);

                _timeToRotate -= Time.deltaTime;
                if (_timeToRotate < 1.5f)
                {
                    //shake
                }
                yield return null;
            }
            _canRotate = true;
            yield return 0;
        }

    }
}
