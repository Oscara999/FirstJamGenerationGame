using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Character.Command;

namespace Core.Character
{

    public class Controller : MonoBehaviour
    {
        public const float TIME_TO_RECORD = 0.8f; 

        [SerializeField] ActionRecorder actionRecorder;
        [SerializeField] Player player;
        [SerializeField] float timeToRecord;
        [SerializeField] GameObject soulPlaceholder;
        [SerializeField] float jumpSpeed = 5.0f;
        [SerializeField] float verticalVelocityThreshold = 2.0f;
        [SerializeField] float gravityMultiplier = 0.4f;
        public float speed = 8.0f;
        public bool canMove = true;
        private CharacterController characterController;
        private Rigidbody  rigidBody;
        private bool canJump = true;
        private bool record = false;
        private bool canRewind = false;
        private bool canRecord = true;
        private bool reached = false;
        private Vector3 inputHolder;
        private float speedHolder;

        void OnEnable()
        {
            Player.onPreviousReached += PreviousReached;
        }
        void OnDisable()
        {
            Player.onPreviousReached -= PreviousReached;    
        }

        private void PreviousReached(bool reached)
        {
            soulPlaceholder.SetActive(false);
            //removeSoulPlaceholder = reached;
            actionRecorder.actions.Clear();
            record = false;
            reached = true;
            canMove = true;
            canRecord = true;
            // rigidBody.isKinematic = false;
            // rigidBody.useGravity = false;
            return;
        }
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            rigidBody = GetComponent<Rigidbody>();
            timeToRecord = TIME_TO_RECORD;
            speedHolder = speed;
        }
        void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (Input.GetKeyDown(KeyCode.E)  && canRecord)
            {
                soulPlaceholder.SetActive(true);
                soulPlaceholder.transform.position = transform.position;
                record = !record;
                canRewind = true;
                canRecord = false;
            }
            if ( Input.GetKeyDown(KeyCode.X) && !canRecord)
            {
                soulPlaceholder.SetActive(false);
                player.positions.Clear();
                soulPlaceholder.transform.position = transform.position;
                record = false;
                canRewind = false;
                reached = false;
                canRecord = true;
            }
            if (record)
                Record();
        
            if (canRewind)
               SetToRewind();
               
            if (Input.GetKeyDown(KeyCode.Q) && reached)
                Rewind();

            
            inputHolder = new Vector3(horizontalInput, 0, verticalInput);
            // if (horizontalInput != 0 || verticalInput != 0 && canJump)
            //     moveAndJump = true;
            // else
            //     moveAndJump = false;
            if (canMove)
            {
                rigidBody.AddForce(new Vector3(horizontalInput* speed * Time.deltaTime, 0, verticalInput * speed * Time.deltaTime), ForceMode.VelocityChange);
                // rigidBody.velocity = Vector3.ClampMagnitude( inputHolder * speed, 6 );
                if (Mathf.Abs(rigidBody.velocity.x) >= 6 || Mathf.Abs(rigidBody.velocity.z) >= 6)
                    rigidBody.AddForce(new Vector3(horizontalInput*- speed * Time.deltaTime, 0, verticalInput * -speed * Time.deltaTime), ForceMode.VelocityChange);

            }
            //     rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity + inputHolder + new Vector3(0, rigidBody.velocity.y, 0), speed );
           
            // else if (!canJump && canMove)

            // if (horizontalInput != 0 || verticalInput != 0 && canJump)
            // {
            //     if (Input.GetButtonDown("Jump") && canJump)
            //     {
            //         rigidBody.AddForce(new Vector3(0, jumpSpeed * 2, 0), ForceMode.Impulse);
            //         canJump = false;
            //     }
            // }
                  
            if (Input.GetButtonDown("Jump") && canJump)
            {
                rigidBody.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
                canJump = false;
            }
            

            // if (rigidBody.velocity.y < verticalVelocityThreshold)
            //     rigidBody.velocity -= Vector3.up * gravityMultiplier;
            

        }

        void Record()
        {
            record = false;
            var moveAction = new MoveAction(player, transform.position, speed + 5, actionRecorder);
            actionRecorder.Record(moveAction);
            StartCoroutine(ActionRecorder());
        }
        void Rewind()
        {
            StopAllCoroutines();
            record = false;
            reached = false;
            actionRecorder.Rewind();
            canMove = false;
        }
        void SetToRewind()
        {
            reached = true;
            canRewind = false;
            canRecord = false;
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Floor"))
            {
                canJump = true;
                
            }
        }
        
        IEnumerator ActionRecorder()
        {
            
            while(timeToRecord >= 0)
            {
                timeToRecord -= Time.deltaTime;
                yield return null;
            }
            timeToRecord = TIME_TO_RECORD;
            record = true;

        }

       

    }


}