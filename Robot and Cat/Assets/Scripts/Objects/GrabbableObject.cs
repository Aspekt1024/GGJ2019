using RobotCat.Audio;
using RobotCat.Player;
using UnityEngine;

namespace RobotCat.Objects
{
    public enum GrabbableObjects
    {
        Cup, Book, 
    }


    public class GrabbableObject : MonoBehaviour
    {
        public void SpawnCup(SpawnLocation spawnLocation)
        {
            GetComponentInChildren<MeshCollider>().enabled = true;
            GetComponentInChildren<MeshRenderer>().enabled = true;
            transform.position = spawnLocation.transform.position;
            resetCup();
        }

        public void DeactivateCup()
        {
            GetComponentInChildren<MeshCollider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;

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
            else
            {
                ScoreManager.instance.reBattedObject();
            }
            struck = true;
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

            if (RCStatics.SFX.SFXDelay > Time.timeSinceLevelLoad) return;

            if (ObjectType == GrabbableObjects.Cup)
            {
                if (collision.collider.GetComponent<Floors>())
                {

                    if (!hitTheFloor)
                    {
                        ScoreManager.instance.flooredObject();
                    }
                    hitTheFloor = true;
                    if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > 0.1f)
                    {
                        RCStatics.SFX.PlayRandom(SFX.CupImpactFloor);
                    }
                }
                else if (collision.collider.tag == "Wall")
                {
                    RCStatics.SFX.PlayRandom(SFX.CupImpactWall);
                }
                else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Surface"))
                {
                    RCStatics.SFX.PlayRandom(SFX.CupTable);
                }
                else if (collision.collider.GetComponent<GrabbableObject>()?.ObjectType == GrabbableObjects.Cup)
                {
                    if (GetComponent<Rigidbody>().velocity.magnitude > 0.5f)
                    {
                        RCStatics.SFX.PlayRandom(SFX.CupImpactMug);
                    }
                }
            }
            else
            {

                if (collision.collider.GetComponent<Floors>())
                {

                    if (!hitTheFloor)
                    {
                        ScoreManager.instance.flooredObject();
                    }
                    hitTheFloor = true;
                }
                RCStatics.SFX.PlayRandom(SFX.PawCup);
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
