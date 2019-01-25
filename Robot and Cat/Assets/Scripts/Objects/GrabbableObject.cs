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
        public GrabbableObjects ObjectType;
        private bool held = false;
        private Transform holder;
        private Rigidbody rigidbody;
        public void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        public void Update()
        {
            if (held)
            {
                gameObject.transform.position = holder.position;
            }
        }
        public void heldBy(Transform holderTransform)
        {
            holder = holderTransform;
            held = true;
            //rigidbody.isKinematic = true;
        }
        
        public void release()
        {
            held = false;
            rigidbody.velocity = Vector3.zero;
            //rigidbody.isKinematic = false;
        }
    }
}
