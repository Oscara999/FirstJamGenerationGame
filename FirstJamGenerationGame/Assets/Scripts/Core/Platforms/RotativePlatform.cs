using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Platforms
{
    public class RotativePlatform : Platform
    {
        [SerializeField] float turnSpeed = 5.0f;
        [SerializeField] float[] randomRotationTimes = {2.4f, 3.5f};
        private bool canRotate = true;
        private float timeToRotate;
        protected override void Behavior()
        {

            if (canRotate)
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
            canRotate = false;
            timeToRotate = UnityEngine.Random.Range(randomRotationTimes[0], randomRotationTimes[1]);
            while(timeToRotate > 0)
            {
                transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime, Space.Self);

                timeToRotate -= Time.deltaTime;
                if (timeToRotate < 1.5f)
                {
                    //shake
                }
                yield return new WaitForSeconds(timeToRotate);
            }
            canRotate = true;
            yield return 0;
        }

    }
}
