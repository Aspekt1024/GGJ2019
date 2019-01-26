using RobotCat.Objects;
using UnityEngine;

namespace RobotCat.Player
{
    public class GrabComponent : MonoBehaviour
    {
        public Transform ObjectHoldPositioner;

        private GrabbableObject currentHeld;
        private GrabbableObject currentFocus;

        private enum States
        {
            HoldingObject, EmptyHands
        }
        private States state = States.EmptyHands;

        private void Update()
        {
            switch (state)
            {
                case States.HoldingObject:

                    var placemat = GetPlacemat();
                    if (placemat == null)
                    {
                        currentHeld.transform.position = ObjectHoldPositioner.position;
                    }
                    else
                    {
                        placemat.positionObject(currentHeld);
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        DropObject();
                    }
                    break;
                case States.EmptyHands:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        TryGrabObject();
                    }
                    break;
                default:
                    break;
            }


            switch (state)
            {
                case States.HoldingObject:
                    break;
                case States.EmptyHands:
                    CheckForGrabbable();
                    break;
                default:
                    break;
            }
        }
        

        private PlacematComponent GetPlacemat()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f, LayerUtil.GetLayerMask(Layers.ObjectPlacemat));
            if (hitObject)
            {
                return hit.collider.gameObject.GetComponentInParent<PlacematComponent>();
            }
            return null;
        }

        private void CheckForGrabbable()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f, LayerUtil.GetLayerMask(Layers.GrabbableObject));
            // TODO illuminate object
        }

        private void TryGrabObject()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f, LayerUtil.GetLayerMask(Layers.GrabbableObject));
            if (hitObject)
            {
                Debug.Log(hit.collider.gameObject.name);
                var grabbable = hit.collider.gameObject.GetComponentInParent<GrabbableObject>();
                if(grabbable != null)
                {
                    state = States.HoldingObject;
                    currentHeld = grabbable;
                    currentHeld.gravityOff();
                }
            }
        }

        private void DropObject()
        {
            currentHeld.gravityOn();
            currentHeld.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentHeld.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            currentHeld = null;
            state = States.EmptyHands;

        }
    }
}
