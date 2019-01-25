using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RobotCat
{
    public class RCStatics
    {
        private static RCStatics instance;

        private GameManager gameManager;

        public RCStatics(GameManager gameManager)
        {
            if (instance != null)
            {
                Debug.LogError($"Detected multiple versions of {nameof(RCStatics)}");
            }

            instance = this;
        }



    }
}
