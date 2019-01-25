using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotCat
{
    public class GameManager : MonoBehaviour
    {
        private RCStatics statics;


        void Awake()
        {
            statics = new RCStatics(this);
        }

        void Update()
        {

        }
    }
}


