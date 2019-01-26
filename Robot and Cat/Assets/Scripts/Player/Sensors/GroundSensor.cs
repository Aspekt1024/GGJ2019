using RobotCat.UI;
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
        private Collider collider;

        private float timeLeftGround;
        private const float LATE_JUMP_THRESHOLD = 0.4f;

        public GroundSensor(Rigidbody body, Collider collider)
        {
            this.body = body;
            this.collider = collider;
        }

        private enum States
        {
            Grounded, NotGrounded
        }
        private States state;

        public void Tick(float deltaTime)
        {
            bool hitGround = GroundHitCheck();

            if (hitGround && state == States.NotGrounded)
            {
                state = States.Grounded;
            }
            else if (!hitGround && state == States.Grounded)
            {
                timeLeftGround = Time.time;
                state = States.NotGrounded;
            }

            DebugUI.SetText(state.ToString());
        }

        public bool IsOnGround { get { return state == States.Grounded || Time.time < timeLeftGround + LATE_JUMP_THRESHOLD; } }

        private float GetLowerExtent()
        {
            const float threshold = 0.05f;
            return collider.bounds.extents.y + threshold;
        }

        private bool GroundHitCheck()
        {
            Ray ray = new Ray(body.transform.position, Vector3.down);

            float dist = GetLowerExtent();

            float xMin = -collider.bounds.extents.x;
            float xMax = -collider.bounds.extents.x;
            float yMin = -collider.bounds.extents.y;
            float yMax = -collider.bounds.extents.y;

            const int gridSplit = 3; // Must be odd for now

            var pos = body.transform.position;

            for (int x = -(gridSplit - 1) / 2; x < (gridSplit - 1) / 2; x++)
            {
                for (int y = -(gridSplit - 1) / 2; y < (gridSplit - 1) / 2; y++)
                {
                    float xPos = pos.x + x * collider.bounds.extents.x;
                    float yPos = pos.y + y * collider.bounds.extents.y;

                    if (Physics.Raycast(ray, dist, LayerUtil.GetLayerMask(Layers.Surface)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void OnJump()
        {
            // we don't want to double jump
            timeLeftGround = 0f;
        }
    }
}
