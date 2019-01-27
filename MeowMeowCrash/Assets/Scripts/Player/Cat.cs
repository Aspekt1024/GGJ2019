using UnityEngine;

namespace RobotCat.Player
{
    public class Cat : PlayerBase
    {
        public GameObject Model;

        private void Start()
        {
            Model.SetActive(false);
        }
    }
}
