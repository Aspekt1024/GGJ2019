using RobotCat.UI;
using UnityEngine;

namespace RobotCat
{
    public class RCStatics
    {
        private static RCStatics instance;

        private GameManager gameManager;
        private ScoreManager score;
        private UIManager ui;

        public RCStatics(GameManager gameManager)
        {
            if (instance != null)
            {
                Debug.LogError($"Detected multiple versions of {nameof(RCStatics)}. This should only be created at runtime.");
                return;
            }

            instance = this;
            this.gameManager = gameManager;
            score = new ScoreManager();
            ui = Object.FindObjectOfType<UIManager>();
        }

        public void Tick()
        {
            score.Tick();
        }

        public static GameManager GameManager { get { return instance.gameManager; } }
        public static ScoreManager Score { get { return instance.score; } }
        public static UIManager UI { get { return instance.ui; } }

    }
}
