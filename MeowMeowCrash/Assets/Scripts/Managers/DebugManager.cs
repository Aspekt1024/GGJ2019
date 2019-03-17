using UnityEngine;

namespace RobotCat
{
    public class DebugManager : MonoBehaviour
    {
        public bool DebugMode = false;

        private void Update()
        {
            if (!DebugMode) return;

            if (Input.GetKeyDown(KeyCode.F5))
            {
                RCStatics.GameManager.GameOver();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                RCStatics.Score.SetScoreDebug(12311);
                RCStatics.GameManager.GameOver();
            }
        }
    }
}
