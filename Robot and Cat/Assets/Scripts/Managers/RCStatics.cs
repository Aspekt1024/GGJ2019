using UnityEngine;

namespace RobotCat
{
    public class RCStatics
    {
        private static RCStatics instance;

        private GameManager gameManager;
        private PhaseManager phase;
        private ScoreManager score;

        public RCStatics(GameManager gameManager)
        {
            if (instance != null)
            {
                Debug.LogError($"Detected multiple versions of {nameof(RCStatics)}. This should only be created at runtime.");
                return;
            }

            instance = this;
            this.gameManager = gameManager;
            phase = new PhaseManager();
            score = new ScoreManager();
        }

        public void Tick()
        {
            phase.Tick();
        }

        public static GameManager GameManager { get { return instance.gameManager; } }
        public static PhaseManager Phase { get { return instance.phase; } }
        public static ScoreManager Score { get { return instance.score; } }

    }
}
