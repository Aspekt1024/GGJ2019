using RobotCat.Player;
using UnityEngine;



public class PhaseManager {
    private enum PossiblePhases
    {
        catPhase, robotPhase
    }
    private Cat cat;
    private Robot robot;
    private PossiblePhases currentPhase;

    public PhaseManager()
    {
        cat = Object.FindObjectOfType<Cat>();
        robot = Object.FindObjectOfType<Robot>();

        cat.gameObject.SetActive(true);
        robot.gameObject.SetActive(false);
        LoadCatPhase();
    }

    public void Tick()
    {


        if (Input.GetKeyDown(KeyCode.Z))
        {
            nextPhase();
        }

    }
    private void LoadCatPhase()
    {
        robot.gameObject.SetActive(false);
        cat.gameObject.SetActive(true);
        currentPhase = PossiblePhases.catPhase;
    }

    private void LoadRobotPhase()
    {
        cat.gameObject.SetActive(false);
        robot.gameObject.SetActive(true);
        currentPhase = PossiblePhases.robotPhase;
    }

    public void nextPhase()
    {
        if (currentPhase == PossiblePhases.catPhase)
        {
            LoadRobotPhase();

        }
        else
        {
            LoadCatPhase();
        }
    }


}
