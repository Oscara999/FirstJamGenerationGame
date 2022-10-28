using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.Character.Command;
namespace Core.Character
{

    public class Player : MonoBehaviour
    {
        public static Action<bool> onPreviousReached;
        public List<Vector3> positions = new List<Vector3>();
        private bool rewind = false;
        private Vector3 prevPosition;
        private float speed;
        private Rigidbody rigidBody;
        private int currentWaypoint;

        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            currentWaypoint = 0;
        }
        public void SetRewind(Vector3 prevPosition, float speed, ActionRecorder _actionRecorder)
        {
            if (_actionRecorder.actions.Count == 0) return;
            rewind = true;
            this.prevPosition = prevPosition;
            this.speed = speed;
            rigidBody.useGravity = false;
            rigidBody.isKinematic = false;
            onPreviousReached?.Invoke(false);

        }
        public void PopulatePositionsList(Vector3 newPosition)
        {
            positions.Insert(0, newPosition);
        }

        private void MoveToPrevious()
        {
            if (!rewind)
                positions.Reverse();

            if (positions.Count ==  0)
            {
                rewind = false;
                rigidBody.useGravity = false;
                rigidBody.isKinematic = true;
                onPreviousReached?.Invoke(!rewind);
                return;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, positions[currentWaypoint], speed * Time.deltaTime);
            
            if (Vector3.Distance(positions[currentWaypoint], transform.position) <= 0)
                positions.RemoveAt(0);
            
        }
        void Update()
        {
            if (rewind)
                MoveToPrevious();
        }
    }


}