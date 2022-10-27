using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character.Command;

namespace Core.Character
{

    public class Controller : MonoBehaviour
    {
        public const float TIME_TO_RECORD = 0.8f; 
        [SerializeField] ActionRecorder _actionRecorder;
        [SerializeField] Player _player;
        [SerializeField] float _timeToRecord;
        public float Speed = 15.0f;
        public bool CanMove = true;
        private CharacterController _characterController;
        private Rigidbody  _rigidBody;
        private bool _canJump = true;
        private bool _record = true;
        private bool _reached = true;

        void OnEnable()
        {
            Player.OnPreviousReached += PreviousReached;
        }
        void OnDisable()
        {
            Player.OnPreviousReached -= PreviousReached;    
        }

        private void PreviousReached(bool reached)
        {
            _reached = reached;
        }
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _rigidBody = GetComponent<Rigidbody>();
            _timeToRecord = TIME_TO_RECORD;
        }
        void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            //transform.position += 
            // if (direction.magnitude >= 0.1f)
            // {
            //     _characterController.Move(direction * speed * Time.deltaTime);
            // }
            
            // if (Input.GetKeyDown(KeyCode.X))
            // {
            // }

            if (_record)
            {
                _record = false;
                var moveAction = new MoveAction(_player, transform.position, Speed + 5);
                _actionRecorder.Record(moveAction);
                StartCoroutine(ActionRecorder());
            }

            if (Input.GetKey(KeyCode.R) && _reached)
            {
                StopAllCoroutines();
                _record = false;
                _actionRecorder.Rewind();
                CanMove = false;
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                _record = true;
                _reached = false;
                CanMove = true;

            }
            if (CanMove)
            {
                //_rigidBody.AddForce(new Vector3(horizontalInput, 0, verticalInput), ForceMode.Impulse);
                _rigidBody.velocity = new Vector3(Mathf.Clamp(horizontalInput * Speed, -Speed, Speed), _rigidBody.velocity.y, Mathf.Clamp(verticalInput * Speed, -Speed, Speed)); //Vector3.ClampMagnitude(_rigidBody.velocity, speed );
                //_rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, speed);

            }
                // _rigidBody.velocity = new Vector3(horizontal * speed , _rigidBody.velocity.y, vertical * speed );
            if (Input.GetButtonDown("Jump") && _canJump)
            {
                _rigidBody.AddForce(new Vector3(0, 8, 0), ForceMode.Impulse);
                //_rigidBody.velocity = new Vector3(horizontalInput * speed , _verticalSpeed, verticalInput * speed );
                
                _canJump = false;
                // _characterController.Move(new Vector3(horizontal , 6 * speed, vertical )  * Time.deltaTime);
            }

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Floor"))
            {
                _canJump = true;
            }
        }
        
        IEnumerator ActionRecorder()
        {
            
            while(_timeToRecord >= 0)
            {
                _timeToRecord -= Time.deltaTime;
                yield return null;
            }
            _timeToRecord = TIME_TO_RECORD;
            _record = true;

        }
    }


}