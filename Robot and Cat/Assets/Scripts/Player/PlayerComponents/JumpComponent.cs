using UnityEngine;

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
            groundSensor = new GroundSensor(body);
        }

        private void FixedUpdate()
        {
            groundSensor.Tick(Time.fixedDeltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && groundSensor.IsOnGround)
            {
                Jump();
            }
        }

        private void Jump()
        {
            body.velocity = new Vector3(body.velocity.x, JumpSpeed, body.velocity.z);
        }
    }
}
