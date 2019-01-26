using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Player
{
    public class Cat : PlayerBase
    {
        public GameObject Model;

        private void Start()
        {
            Model.SetActive(false);
        }
    }
}
