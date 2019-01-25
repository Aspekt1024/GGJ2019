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
        }

        private void Update()
        {
            statics.Tick();
        }
    }
}


