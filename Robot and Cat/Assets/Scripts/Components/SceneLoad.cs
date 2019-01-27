using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RobotCat;
public class SceneLoad : MonoBehaviour {
    void Update()
    {
        if(Input.anyKeyDown)
        {
            Debug.Log("ADSFASf");
            Debug.Log(MainMenuTransistion.instance.transistioning);
        }
        if ((Input.anyKeyDown) && (!MainMenuTransistion.instance.transistioning))
        {
            MainMenuTransistion.instance.gameOut();
        }
    }
}
