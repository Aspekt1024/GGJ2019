using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class JumpComponent : MonoBehaviour
    {
        public float JumpSpeed;

        private Rigidbody body;

        [SerializeField]
        private GameObject floor;
        
        private bool jumped = false;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                body.velocity = new Vector3(body.velocity.x, JumpSpeed, body.velocity.z);
            }
        }
/*        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.Equals(floor) && jumped == true)
            {
                jumped = false;
            }
        }*/
    }
}
