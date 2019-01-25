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

        private enum States
        {
            HoldingObject, EmptyHands
        }
        private States state;

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

        private void CheckforObject()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f);

            if (hitObject)
            {
                var grabbable = hit.collider.gameObject.GetComponentInParent<GrabbableObject>();
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

            }
        }

        private void DropObject()
        {

        }
    }
}
