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
        private GameObject currentFocus;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckforObject();   
            }   
        }

        private void CheckforObject()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hitObject = Physics.Raycast(ray, out hit, 3f);

            if (hitObject && hit.collider.gameObject.layer == LayerMask.NameToLayer("GrabbableObject"))
            {
                currentHeld = hit.collider.gameObject.GetComponentInParent<GrabbableObject>();
                if (currentHeld != null)
                {
                    currentHeld.transform.position = ObjectHoldPositioner.position;
                }
            }
        }
    }
}
