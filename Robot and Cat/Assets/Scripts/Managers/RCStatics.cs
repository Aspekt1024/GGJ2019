using UnityEngine;

namespace RobotCat
{
    public class RCStatics
    {
        private static RCStatics instance;

        private GameManager gameManager;
        private PhaseManager phase;

        public RCStatics(GameManager gameManager)
        {
            if (instance != null)
            {
                Debug.LogError($"Detected multiple versions of {nameof(RCStatics)}. This should only be created at runtime.");
                return;
            }

            instance = this;
            phase = new PhaseManager();
        }

        public void Tick()
        {
            phase.Tick();
        }

        public static GameManager GameManager { get { return instance.gameManager; } }
        public static PhaseManager Phase { get { return instance.phase; } }

    }
}
