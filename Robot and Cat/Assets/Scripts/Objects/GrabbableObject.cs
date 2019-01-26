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
            //Set the cup to the location of it's allocated spawn
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
            collided = true;
        }

        public void struckByCat()
        {
            //call score manager
            struck = true;
        }

        public void collidedWithFloor()
        {
            //Call score manager
            collided = true;
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
