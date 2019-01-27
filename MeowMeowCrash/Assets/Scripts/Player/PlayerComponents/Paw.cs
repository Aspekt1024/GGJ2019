using RobotCat.Audio;
using RobotCat.Objects;
using UnityEngine;

namespace RobotCat.Player
{
    public class Paw : MonoBehaviour
    {
        public float SwipeForce = 500f;

        private void OnTriggerEnter(Collider other)
        {
            var body = other.GetComponentInParent<Rigidbody>();
            if (body == null)
            {
                body = other.GetComponent<Rigidbody>();
            }

            if (body != null)
            {
                body.velocity = GetComponentInParent<Cat>().transform.right * (name == "Right" ? -1 : 1) * SwipeForce;
                body.GetComponent<GrabbableObject>()?.struckByCat();
                RCStatics.SFX.PlayRandom(SFX.PawCup);
            }
        }
    }
}
