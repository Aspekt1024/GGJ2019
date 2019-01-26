using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class MovementComponent : MonoBehaviour
    {
        public float MoveSpeed = 10f;

        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            float xVel = hAxis * MoveSpeed;
            float zVel = vAxis * MoveSpeed;

            var forwardVel = transform.forward * vAxis;
            var sideVel = transform.right * hAxis;

            var vel = (forwardVel + sideVel).normalized * MoveSpeed;
            
            body.velocity = new Vector3(vel.x, body.velocity.y, vel.z);

        }



    }
}
