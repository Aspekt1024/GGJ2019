using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RobotCat.UI
{
    public class UIManager : MonoBehaviour
    {
        public Image ExcitementBar;

        public void SetExcitement(float value)
        {
            ExcitementBar.fillAmount = value;
        }
    }
}
