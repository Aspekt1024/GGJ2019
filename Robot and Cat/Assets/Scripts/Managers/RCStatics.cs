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
                Debug.LogError($"Detected multiple versions of {nameof(RCStatics)}. This should only be created at runtime.");
                return;
            }

            instance = this;
        }

        public static GameManager GameManager { get { return instance.gameManager; } }

    }
}
