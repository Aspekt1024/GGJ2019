using RobotCat.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Objects
{
    public enum GrabbableObjects
    {
        Cup,
    }


    public class GrabbableObject : MonoBehaviour
    {
        private void Update()
        {

        }

        public GrabbableObjects ObjectType;
        public void gravityOn()
        {
            GetComponent<Rigidbody>().useGravity = true;
        }

        public void gravityOff()
        {
            GetComponent<Rigidbody>().useGravity = false;

        }

        private void OnCollisionExit(Collision collision)
        {
            var c = collision.collider.gameObject.GetComponent<Cat>();
            if (c == null)
            {
                c = collision.collider.GetComponentInParent<Cat>();
            }

            if (c != null)
            {
                Debug.Log("Touched by cat");
                RCStatics.Score.Track(gameObject);
            }
        }
    }
}
