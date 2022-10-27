using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Platforms
{
    public class MovingPlatform : Platform
    {
        [SerializeField] private List<GameObject> _waypoints;
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] Transform _parent;
        private int _currentWaypoint;
        // Start is called before the first frame update
        private void Start()
        {
            if (_waypoints.Count <= 0) return;
            _currentWaypoint = 0;
        }

        protected override void Behavior()
        {
        
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].transform.position,
                (moveSpeed * Time.deltaTime));

            if (Vector3.Distance(_waypoints[_currentWaypoint].transform.position, transform.position) <= 0)
            {
                _currentWaypoint++;
            }

            if (_currentWaypoint != _waypoints.Count) return;
                _waypoints.Reverse();
            _currentWaypoint = 0;
        }


        private void OnTriggerEnter(Collider other)
        {
            Vector3 playerScale = other.gameObject.transform.localScale;
            if (!other.CompareTag("Player")) return;
                
                // other.transform.SetParent(_parent);
                other.transform.parent = transform;
                // other.transform.localScale = new Vector3(3/transform.localScale.x, 1/transform.localScale.y,3/transform.localScale.z);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            other.transform.SetParent(null);
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Vector3[] _segmentsArray = new Vector3[_waypoints.Count + 1];
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
            DrawMovementLine(_segmentsArray[0], _waypoints);
        }
#endif

       
    }

}