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
        public Color maxexcitement;
        public Color minexcitement;
        public Image ExcitementBar;
        public MenuScripts Menu;

        private CreditsMenuScript creditsMenu;

        private void Awake()
        {
            creditsMenu = FindObjectOfType<CreditsMenuScript>();
            creditsMenu?.Hide();
        }

        public void SetExcitement(float value)
        {
            ExcitementBar.fillAmount = value;
        }

        public void ToggleMenu()
        {
            Menu.Toggle();
        }

        public void HideMenu()
        {
            Menu.Hide();
        }

        public void ShowCredits()
        {
            creditsMenu.Show();
        }

        public void HideCredits()
        {
            creditsMenu.Hide();
        }
    }
}
