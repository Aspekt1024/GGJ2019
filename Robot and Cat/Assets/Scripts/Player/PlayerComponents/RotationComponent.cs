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
        [SerializeField]
        public float sensitivity = 5.0f;
        [SerializeField]
        public float smoothing = 2.0f;
        // the chacter is the capsule
        public GameObject character;
        // get the incremental value of mouse moving
        private Vector2 mouseLook;
        // smooth the mouse moving
        private Vector2 smoothV;

        public float yMin = -60f;
        public float yMax = 60f;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (RCStatics.GameManager.IsInMenu) return;

            // md is mosue delta
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            // the interpolated float result between the two float values
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            // incrementally add to the camera look
            mouseLook += smoothV;

            // vector3.right means the x-axis
            mouseLook.y = Mathf.Clamp(mouseLook.y, yMin, yMax);
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
        }
}
