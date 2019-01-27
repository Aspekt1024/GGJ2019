using RobotCat.UI;
using UnityEngine;

namespace RobotCat.Player
{
    public class GroundSensor : ISensor
    {
        private Rigidbody body;
        private Collider collider;

        private float timeLeftGround;
        private const float LATE_JUMP_THRESHOLD = 0.4f;

        // TODO manage in jump component
        private const float JUMP_COOLDOWN_TIMER = 0.5f;
        private float timeJumped;

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

        public bool IsOnGround
        {
            get
            {
                if (state == States.Grounded)
                {
                    return true;
                }
                else if (Time.time < timeLeftGround + LATE_JUMP_THRESHOLD && Time.time > timeJumped + JUMP_COOLDOWN_TIMER)
                {
                    return true;
                }
                return false;
            }
        }

        private float GetLowerExtent()
        {
            const float threshold = 0.05f;
            return collider.bounds.extents.y + threshold;
        }

        private bool GroundHitCheck()
        {
            Vector3 rayPos = body.transform.position;
            Ray ray = new Ray(rayPos, Vector3.down);

            float dist = GetLowerExtent();

            const int gridSplit = 3; // Must be odd for now

            var pos = body.transform.position;

            for (int x = -(gridSplit - 1) / 2; x < (gridSplit - 1) / 2; x++)
            {
                for (int y = -(gridSplit - 1) / 2; y < (gridSplit - 1) / 2; y++)
                {
                    rayPos.x = pos.x + x * collider.bounds.extents.x;
                    rayPos.z = pos.z + y * collider.bounds.extents.y;
                    ray = new Ray(rayPos, Vector3.down);

                    Layers[] layers = new Layers[]
                    {
                        Layers.Surface, Layers.GrabbableObject
                    };
                    if (Physics.Raycast(ray, dist, LayerUtil.GetLayerMask(layers)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void OnJump()
        {
            timeJumped = Time.time;
        }
    }
}
