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

        private float r;
        private float g;
        private float b;

        private float maxExcitementRate;
        private float currentRate;

        private CreditsMenuScript creditsMenu;

        private void Awake()
        {
            creditsMenu = FindObjectOfType<CreditsMenuScript>();
            creditsMenu?.Hide();
        }

        public void SetExcitement(float value)
        {
            currentRate = ScoreManager.instance.excitementDecreaseRate;
            maxExcitementRate = ScoreManager.instance.maxRateOfDecrease;

            r = Mathf.Lerp(minexcitement.r, maxexcitement.r,1 - currentRate / maxExcitementRate);

            g = Mathf.Lerp(minexcitement.g, maxexcitement.g, 1- currentRate / maxExcitementRate);

            b = Mathf.Lerp(minexcitement.b, maxexcitement.b, 1- currentRate / maxExcitementRate);

            ExcitementBar.color = new Color(r, g, b, ExcitementBar.color.a);

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
