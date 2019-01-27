using RobotCat.Audio;
using System.Collections;
using UnityEngine;

namespace RobotCat.Player
{
    public class PawComponent : MonoBehaviour
    {
        public Animator LeftPawAnim;
        public Animator RightPawAnim;

        public float SwipeCooldown = 1.3f;

        private enum States
        {
            None, PawOut, Swiping
        }
        private States leftState = States.None;
        private States rightState = States.None;

        private void Update()
        {
            if (RCStatics.GameManager.IsInMenu) return;
            if (Input.GetMouseButtonDown(0))
            {
                PawLeft();
            }
            if (Input.GetMouseButtonDown(1))
            {
                PawRight();
            }
            
        }

        private void PawRight()
        {
            if (leftState == States.Swiping) return;
            leftState = States.Swiping;
            RightPawAnim.Play("SwipeLeft", 0, 0f);
            StartCoroutine(DelayAfterSwipe(SwipeCooldown, true));

            RCStatics.SFX.PlayRandom(SFX.PawSwipe);
        }
        private void PawLeft()
        {
            if (rightState == States.Swiping) return;
            rightState = States.Swiping;
            LeftPawAnim.Play("SwipeRight", 0, 0f);
            StartCoroutine(DelayAfterSwipe(SwipeCooldown, false));

            RCStatics.SFX.PlayRandom(SFX.PawSwipe);
        }

        private void PawIn()
        {
            leftState = States.None;
            // TODO animate
        }

        private void Swipe()
        {
            leftState = States.Swiping;
        }
        
        private IEnumerator DelayAfterSwipe(float delay, bool isLeftPaw)
        {
            yield return new WaitForSeconds(delay);

            if (isLeftPaw)
            {
                leftState = States.None;
            }
            else
            {
                rightState = States.None;
            }
        }
    }
}
