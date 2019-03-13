using TMPro;
using UnityEngine;

namespace RobotCat.UI
{
    public class UIManager : MonoBehaviour
    {
        // TODO these belong in the excitementBar
        public Color maxexcitement;
        public Color minexcitement;
        public MenuScripts Menu;
        public ScoreUI Score;
        public ExcitementBar ExcitementBar;
        public HighScoreUI HighScore;
        
        private float maxExcitementRate;
        private float currentRate;
        
        public void SetExcitement(float value)
        {
            // TODO this doesn't belong in the UI and should be passed as a ratio
            currentRate = ScoreManager.instance.ExcitementDecreaseRate;
            maxExcitementRate = ScoreManager.instance.MaxRateOfDecrease;

            var col = Color.Lerp(minexcitement, maxexcitement, 1 - currentRate / maxExcitementRate);
            ExcitementBar.SetValue(value, col);
        }

        public void ToggleMenu()
        {
            Menu.Toggle();
        }

        public void HideMenu()
        {
            Menu.Hide();
        }
    }
}
