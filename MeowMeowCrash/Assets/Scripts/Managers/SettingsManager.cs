﻿using UnityEngine;

namespace RobotCat
{
    public class SettingsManager : MonoBehaviour
    {
        public float MouseSensitivityMin = 0.5f;
        public float MouseSensitivityMax = 10f;

        public bool EndlessMode = false;

        [SerializeField] private float mouseSensitivityFactor = 0.5f;

        private static SettingsManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                instance = this;
                DestroyImmediate(gameObject);
                return;
            }

            Object.DontDestroyOnLoad(this);
        }

        /// <summary>
        /// The calculated sensitivity
        /// </summary>
        public float Sensitivity
        {
            get
            {
                float sensitivity = Mathf.Lerp(MouseSensitivityMin, MouseSensitivityMax, mouseSensitivityFactor);
#if WEB_GL
                sensitivity *= 0.5f;
#endif
                return sensitivity;
            }
        }

        public void SetSensitivity(float value)
        {
            mouseSensitivityFactor = value;
        }

        public float GetMouseSensitivityFactor()
        {
            return mouseSensitivityFactor;
        }
    }
}