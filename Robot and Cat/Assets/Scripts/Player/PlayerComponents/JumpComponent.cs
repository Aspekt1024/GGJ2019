using UnityEngine;
using System.Collections;

namespace RobotCat.Player
{
    public class JumpComponent : MonoBehaviour
    {
        public float JumpSpeed;

        private Rigidbody body;
        private GroundSensor groundSensor;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            var collider = GetComponentInChildren<Collider>();
            groundSensor = new GroundSensor(body, collider);
        }

        private void FixedUpdate()
        {
            if (RCStatics.GameManager.IsInMenu) return;
            groundSensor.Tick(Time.fixedDeltaTime);

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C)) && groundSensor.IsOnGround)
            {
                Jump();
            }
        }

        private void Jump()
        {
            groundSensor.OnJump();
            body.velocity = new Vector3(body.velocity.x, JumpSpeed, body.velocity.z);
        }
    }
}
