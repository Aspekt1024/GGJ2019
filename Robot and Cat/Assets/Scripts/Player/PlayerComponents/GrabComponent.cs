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
        private PlacematComponent currentPlacemat = null;

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


            switch (state)
            {
                case States.HoldingObject:
                    UpdatePlacemat();
                    currentHeld.gravityOff();
                    if (currentPlacemat == null)
                    {
                        currentHeld.transform.position = ObjectHoldPositioner.position;
                    }
                    else
                    {
                        currentPlacemat.positionObject(currentHeld);
                    }
                    break;
                case States.EmptyHands:
                    CheckForGrabbable();
                    break;
                default:
                    break;
            }
        }

        public void FixedUpdate()
        {
        }

        private void UpdatePlacemat()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f, LayerUtil.GetLayerMask(Layers.ObjectPlacemat));
            if (hitObject)
            {
                currentPlacemat = hit.collider.gameObject.GetComponentInParent<PlacematComponent>();
            }
            else
            {
                currentPlacemat = null;
            }
        }

        private void CheckForGrabbable()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f, LayerUtil.GetLayerMask(Layers.GrabbableObject));
            // TODO illuminate object
        }

        private void CheckforObject()
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
                }
            }
        }

        private void DropObject()
        {
            currentHeld.gravityOn();
            currentHeld = null;
            state = States.EmptyHands;

        }
    }
}
