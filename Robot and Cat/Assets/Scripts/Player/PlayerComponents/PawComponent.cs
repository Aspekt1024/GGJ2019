using UnityEngine;

namespace RobotCat.Player
{
    public class PawComponent : MonoBehaviour
    {
        public Transform Paw;

        private Animator anim;

        private enum States
        {
            None, PawOut, Swiping
        }
        private States state = States.None;

        private void Awake()
        {
            anim = Paw.GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PawOut();
            }
            
        }

        private void PawOut()
        {
            state = States.PawOut;
            anim.Play("Swipe", 0, 0f);
        }
        
        private void PawIn()
        {
            state = States.None;
            // TODO animate
        }

        private void Swipe()
        {
            state = States.Swiping;
        }
        
    }
}
