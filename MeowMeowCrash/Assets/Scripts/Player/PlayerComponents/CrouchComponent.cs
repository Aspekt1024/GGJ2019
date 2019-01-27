using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class CrouchComponent : MonoBehaviour
    {
        public float NormalPosition = 1.5f;
        public float CrouchedPosition = 0.7f;

        private enum States
        {
            Normal, Crouched
        }
        private States state;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                switch (state)
                {
                    case States.Normal:
                        Crouch();
                        break;
                    case States.Crouched:
                        Uncrouch();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Crouch()
        {
            state = States.Crouched;
            var pos = transform.position;
            pos.y = CrouchedPosition;
            transform.position = pos;
        }

        private void Uncrouch()
        {
            state = States.Normal;
            var pos = transform.position;
            pos.y = NormalPosition;
            transform.position = pos;
        }

    }
}
