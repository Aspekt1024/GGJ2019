using System.Collections;
using System.Collections.Generic;
using RobotCat.Player;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreManager()
    {

    }
}

struct ScoreItem
{
    GameObject obj;
    Vector3 initialPos;
    public ScoreItem(GameObject gameobj, Vector3 iniPos)
    {
        obj = gameobj;
        initialPos = iniPos;
    }
}


