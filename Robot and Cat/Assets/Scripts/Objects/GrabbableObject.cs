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
        public void SpawnCup(SpawnLocation spawnLocation)
        {
            GetComponent<MeshCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            transform.position = spawnLocation.transform.position;
            resetCup();
        }

        public void DeactivateCup()
        {
            GetComponent<MeshCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

        }
        //Has the cat pawed it?
        private bool struck = false;

        //Has the cat run in to it?
        private bool collided = false;

        //Has the cup hit the floor?
        private bool hitTheFloor = false;

        public void resetCup()
        {
            struck = false;
            collided = false;
            hitTheFloor = false;
        }

        public void collidedWith()
        {
            //Call the score manager
            if(!collided)
            {
                ScoreManager.instance.collidedObject();
            }
            collided = true;
        }

        public void struckByCat()
        {
            //call score manager
            if(!struck)
            {
                ScoreManager.instance.battedObject();
            }
            struck = true;
        }

        public void collidedWithFloor()
        {
            //Call score manager
            if(!hitTheFloor)
            {
                ScoreManager.instance.flooredObject();
            }
            hitTheFloor = true;
        }


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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.gameObject.GetComponent<Floors>())
            {
                collidedWithFloor();
            }
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
                collidedWith();
            }
        }
    }
}
