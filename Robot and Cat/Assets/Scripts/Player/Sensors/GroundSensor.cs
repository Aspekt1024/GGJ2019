using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class GroundSensor : ISensor
    {
        private Rigidbody body;

        public GroundSensor(Rigidbody body)
        {
            this.body = body;
        }

        private enum States
        {
            Grounded, NotGrounded
        }
        private States state;

        public void Tick(float deltaTime)
        {
            RaycastHit hit;
            Ray ray = new Ray(body.transform.position, Vector3.down);
            float dist = GetLowerExtent();
            bool hitGround = Physics.Raycast(ray, out hit, dist, LayerUtil.GetLayerMask(Layers.Surface));

            if (hitGround && state == States.NotGrounded)
            {
                state = States.Grounded;
            }
            else if (!hitGround && state == States.Grounded)
            {
                state = States.NotGrounded;
            }
        }

        public bool IsOnGround { get { return state == States.Grounded; } }

        private float GetLowerExtent()
        {
            Collider[] colliders = body.GetComponentsInChildren<Collider>();
            float lowPoint = 0f;
            foreach (var c in colliders)
            {
                if (c.bounds.extents.y > lowPoint)
                {
                    lowPoint = c.bounds.extents.y;
                }
            }

            const float threshold = 0.05f;
            return lowPoint + threshold;
        }
    }
}
