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



    public void nextPhase()
    {
        if (currentPhase == PossiblePhases.catPhase)
        {

        }
        else
        {
            LoadCatPhase();
        }
    }


}
