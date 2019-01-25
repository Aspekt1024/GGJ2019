using RobotCat.Player;
using UnityEngine;



public class PhaseManager {

    public Camera catCamera;
    public Camera robotCamera;

    private PlayerBase currentplayer;

    private Cat cat;
    private Robot robot;

    public PhaseManager()
    {
        cat = Object.FindObjectOfType<Cat>();
        robot = Object.FindObjectOfType<Robot>();

        cat.gameObject.SetActive(true);
        robot.gameObject.SetActive(false);
        currentplayer = cat;
    }

    public void Tick()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (currentplayer is Cat)
            {
                currentplayer.gameObject.SetActive(false);
                currentplayer = robot;
            }
            else if (currentplayer is Robot)
            {
                currentplayer.gameObject.SetActive(false);
                currentplayer = cat;
            }
            currentplayer.gameObject.SetActive(true);
        }

    }


}
