using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class RotationComponent : MonoBehaviour
    {
        public float YawSensitivity = 0.15f;
        public float PitchSensitivity = 0.15f;

        private float minX = -360f;
        private float maxX = 360f;

        private float minY = -360f;
        private float maxY = 360f;

        private void FixedUpdate()
        {
            float yAxis = Input.GetAxis("Mouse Y");
            float xAxis = Input.GetAxis("Mouse X");

            float yRotation = Mathf.Clamp(xAxis * YawSensitivity, minY, maxY);
            float xRotation = Mathf.Clamp(-yAxis * PitchSensitivity, minX, maxX);

            transform.localEulerAngles += new Vector3(xRotation, yRotation, 0f);
        }
    }
}
