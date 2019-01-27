using UnityEngine;
using RobotCat;
public class SceneLoad : MonoBehaviour {
    void Update()
    {
        if ((Input.anyKeyDown) && (!MainMenuTransistion.instance.transistioning))
        {
            MainMenuTransistion.instance.gameOut();
        }
    }
}
