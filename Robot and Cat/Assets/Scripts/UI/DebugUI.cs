using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RobotCat.UI
{
    public class DebugUI : MonoBehaviour
    {
        public Text DebugText;

        private static DebugUI instance;

        private void Awake()
        {
            instance = this;
        }

        public static void SetText(string text)
        {
            instance.DebugText.text = text;
        }

    }
}
