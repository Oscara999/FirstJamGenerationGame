using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Platforms
{
    public class MovingPlatform : Platform
    {
        [SerializeField] private List<GameObject> waypoints;
        [SerializeField] private float moveSpeed = 4.0f;
        private int currentWaypoint;
        // Start is called before the first frame update
        private void Start()
        {
            if (waypoints.Count <= 0) return;
            currentWaypoint = 0;
           
        }

        protected override void Behavior()
        {
        
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position,
                (moveSpeed * Time.deltaTime));

            if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) <= 0)
            {
                currentWaypoint++;
            }

            if (currentWaypoint != waypoints.Count) return;
                waypoints.Reverse();
            currentWaypoint = 0;
        }


        private void OnTriggerEnter(Collider other)
        {
            Vector3 playerScale = other.gameObject.transform.localScale;
            if (!other.CompareTag("Player")) return;
            other.transform.parent = transform;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.transform.SetParent(null);
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            
            try
            {
                if (!Application.isPlaying)
                    if (transform.position != waypoints[0].transform.position)
                        transform.position = waypoints[0].transform.position;
                
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Debug.LogWarning("Warning: Remember to set at least the first waypoint for the platform");
                return;
            }
            
            Vector3[] _segmentsArray = new Vector3[waypoints.Count + 1];
            for (int i = 0; i < _segmentsArray.Length; i++)
            {
                _segmentsArray[i] = transform.position;
            }
           
            void DrawMovementLine(Vector3 position, List<GameObject> positionsArray)
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i < positionsArray.Count - 1; i++)
                {
                    Gizmos.DrawLine(positionsArray[i].transform.position, positionsArray[(i+1) % positionsArray.Count].transform.position);
                }
            }

            //_enemyTransform = GetComponent<Transform>();

            // Gizmos.color = Color.red;
            // Gizmos.DrawLine(_enemyTransform.position - (MaxDistance / 2) * Vector3.up, _enemyTransform.position + (MaxDistance / 2) * Vector3.up );
            DrawMovementLine(_segmentsArray[0], waypoints);
        }
#endif

       
    }

}