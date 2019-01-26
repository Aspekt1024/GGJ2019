using RobotCat.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class Paw : MonoBehaviour
    {
        public float SwipeForce = 500f;

        private void OnTriggerEnter(Collider other)
        {
            var body = other.GetComponentInParent<Rigidbody>();
            if (body == null)
            {
                body = other.GetComponent<Rigidbody>();
            }

            if (body != null)
            {
                body.velocity = GetComponentInParent<Cat>().transform.right * -SwipeForce;
                body.GetComponent<GrabbableObject>()?.struckByCat();
            }
        }
    }
}
