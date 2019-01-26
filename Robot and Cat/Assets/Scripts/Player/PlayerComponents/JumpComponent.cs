using UnityEngine;
using System.Collections;

namespace RobotCat.Player
{
    public class JumpComponent : MonoBehaviour
    {
        public float JumpSpeed;
        public float jumpGraceTime = 0.3f;
        public float timeSinceLastCoroutineStart = 0.0f;

        private Rigidbody body;
        private GroundSensor groundSensor;
        private IEnumerator gracePeriod;
        private bool jumpGraceRunning = false;

        public bool jumpReady = true;

        private void Awake()
        {
            gracePeriod = JumpGracePeriod();
            body = GetComponent<Rigidbody>();
            var collider = GetComponentInChildren<Collider>();
            groundSensor = new GroundSensor(body, collider);
        }

        private void FixedUpdate()
        {
            groundSensor.Tick(Time.fixedDeltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && groundSensor.IsOnGround)
            {
                Jump();
            }
            else if(groundSensor.IsOnGround)
            {
                //jumpReady = true;
                //jumpGraceRunning = false;
            }
            else
            {
                //GraceTrigger();
            }
        }

        private void Jump()
        {
            jumpReady = false;
            groundSensor.OnJump();
            body.velocity = new Vector3(body.velocity.x, JumpSpeed, body.velocity.z);
        }

        private void GraceTrigger()
        {
            if(!jumpGraceRunning)
            {
                StartCoroutine(gracePeriod);
            }
        }

        private IEnumerator JumpGracePeriod()
        {
            timeSinceLastCoroutineStart = 0.0f;
            jumpGraceRunning = true;
            while(timeSinceLastCoroutineStart < jumpGraceTime && jumpGraceRunning)
            {
                timeSinceLastCoroutineStart += Time.deltaTime;
                yield return null;
            }
            jumpGraceRunning = false;
            jumpReady = false;

        }

    }
}
