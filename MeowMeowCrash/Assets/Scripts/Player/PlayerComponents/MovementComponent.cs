
using UnityEngine;

namespace RobotCat.Player
{
    public class MovementComponent : MonoBehaviour
    {
        private Rigidbody body;
        public AnimationCurve movementCurve;
        public float maxSpeed = 3.0f;
        public float maxRunSpeed = 5f;
        public float accelerationTime = 0.0f;
        public float timeToMaxSpeed = 1.0f;
        private float timeSinceInput = 0.0f;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }
        
        private float translation;
        private float strafe;

        void Update()
        {
            if (RCStatics.GameManager.IsInMenu) return;

            translation = Input.GetAxis("Vertical");
            strafe = Input.GetAxis("Horizontal");

            Vector3 forwardVel = body.transform.forward;
            Vector3 strafeVel = body.transform.right;
            float resolvedSpeed = 0.0f;

            float speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? maxRunSpeed : maxSpeed;

            if ((Mathf.Abs(translation) > 0.01) || (Mathf.Abs(strafe) > 0.01))
            {
                timeSinceInput += Time.deltaTime/timeToMaxSpeed;
                timeSinceInput = Mathf.Clamp(timeSinceInput, 0.0f, 1.0f);

                accelerationTime = movementCurve.Evaluate(timeSinceInput);
                resolvedSpeed = accelerationTime * speed;

                forwardVel = body.transform.forward * translation;
                strafeVel = body.transform.right * strafe;

            }
            else
            {
                timeSinceInput -= Time.deltaTime/timeToMaxSpeed;
                timeSinceInput = Mathf.Clamp(timeSinceInput, 0.0f, 1.0f);

                accelerationTime = movementCurve.Evaluate(timeSinceInput);
                resolvedSpeed = accelerationTime * speed;
                forwardVel = body.velocity;
                strafeVel = new Vector3(0f, 0f, 0f) ;
            }

            Vector3 vel = strafeVel + forwardVel;
            vel.y = 0f;
            vel = vel.normalized * resolvedSpeed;
            
            body.velocity = new Vector3(vel.x, body.velocity.y, vel.z);
        }
    }
}
