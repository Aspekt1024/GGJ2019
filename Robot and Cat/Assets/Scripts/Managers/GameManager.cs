using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotCat
{
    public class GameManager : MonoBehaviour
    {
        private RCStatics statics;

        private void Awake()
        {
            statics = new RCStatics(this);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            RCStatics.UI.HideMenu();
            statics.OnStart();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RCStatics.UI.ToggleMenu();
            }
        }


    }
}


