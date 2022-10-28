using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Platforms
{
    public class RotativePlatform : Platform
    {
        const float SHAKING_SET_TIME = 1.3f;
        [SerializeField] float turnSpeed = 5.0f;
        [SerializeField] float[] randomRotationTimes = {2.4f, 3.5f};
        [SerializeField] float shakingMagnitude = 0.06f;
        private bool canRotate = true;
        private float timeToRotate;
        private float shakingDuration = 1.0f;
        private int signedRotationId;
        protected override void Behavior()
        {

            if (canRotate)
            {
                RotatePlatform();
                //StartCoroutine(TimeToRotate());
            }
            // 
        }

        void RotatePlatform()
        {
            if (transform.rotation.eulerAngles.x < 0 )
            {
                Debug.Log("dot");
                StartCoroutine(TimeToRotate());
            }
            else
                transform.Rotate(Vector3.right, 180 * Time.deltaTime, Space.Self);
        }
        IEnumerator TimeToRotate()
        {
            canRotate = false;
            timeToRotate = UnityEngine.Random.Range(randomRotationTimes[0], randomRotationTimes[1]);
           
            Vector3 originalPosition = transform.position;
            float elapsed = 0f;
            while (elapsed < shakingDuration)
            {
                Debug.Log(elapsed);
                float x = UnityEngine.Random.Range(-0.1f, 0.1f);
                float y = UnityEngine.Random.Range(-0.1f, 0.1f);
                float z = UnityEngine.Random.Range(-0.1f, 0.1f);
                transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, 
                                    originalPosition.z + z );
                elapsed += Time.deltaTime;
                yield return 0;
            }
            transform.position = originalPosition;
                //shake
            
            transform.Rotate(Vector3.right, 2 * Time.deltaTime, Space.Self);
            //yield return new WaitForSeconds(timeToRotate);
            
            canRotate = true;
            yield return 0;
        }

     

    }
}
