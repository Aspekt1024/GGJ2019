using RobotCat.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class GrabComponent : MonoBehaviour
    {
        public Transform ObjectHoldPositioner;

        private GrabbableObject currentHeld;
        private GrabbableObject currentFocus;
        private Ray ray;
        private bool hitObject;
        private RaycastHit hit;

        private enum States
        {
            HoldingObject, EmptyHands
        }
        private States state;

        public void Start()
        {
            state = States.EmptyHands;
        }
        private void Update()
        {
            switch (state)
            {
                case States.HoldingObject:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        DropObject();
                    }
                    break;
                case States.EmptyHands:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CheckforObject();
                    }
                    break;
                default:
                    break;
            }
        }

        public void FixedUpdate()
        {
            switch (state)
            {
                case States.HoldingObject:
                    CheckForPlacemat();
                    break;
                case States.EmptyHands:
                    CheckForGrabbable();
                    break;
                default:
                    break;
            }
        }

        private void CheckForPlacemat()
        {
            ray = new Ray(transform.position, transform.forward);
            hitObject = Physics.Raycast(ray, out hit, 3f);
        }

        private void CheckForGrabbable()
        {
            ray = new Ray(transform.position, transform.forward);
            hitObject = Physics.Raycast(ray, out hit, 3f);
        }

        private void CheckforObject()
        {
            if (hitObject)
            {
                Debug.Log(hit.collider.gameObject.name);
                var grabbable = hit.collider.gameObject.GetComponentInParent<GrabbableObject>();
                if(grabbable != null)
                {
                    grabbable.heldBy(ObjectHoldPositioner);
                    state = States.HoldingObject;
                    currentHeld = grabbable;
                }
                /*
                if (currentHeld != null)
                {

                }
                if (grabbable == currentHeld)
                {

                }
                else
                {
                    currentHeld = grabbable;
                    if (currentHeld != null)
                    {
                        currentHeld.transform.position = ObjectHoldPositioner.position;
                    }
                }
                */

            }
        }

        private void DropObject()
        {
            currentHeld.release();
            currentHeld = null;
            state = States.EmptyHands;
        }
    }
}
