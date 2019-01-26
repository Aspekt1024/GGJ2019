using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RobotCat.Player;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    private struct ScoreItem
    {
        public GameObject obj;
        Vector3 initialPos;
        public int timesTracked;
        public ScoreItem(GameObject gameobj)
        {
            obj = gameobj;
            initialPos = gameobj.transform.position;
            timesTracked = 0;
        }
    }


    private List<ScoreItem> scoreItems;
    public ScoreManager()
    {
        scoreItems = new List<ScoreItem>();
    }

    public void Track(GameObject gameObject)
    {
        ScoreItem[] items = (ScoreItem[])scoreItems.Where(x => x.obj.Equals(gameObject)).ToArray();
        if (items.Length >0)
        {
            // already in the list
            items[0].timesTracked++;
        }
        else
        {
            scoreItems.Add(new ScoreItem(gameObject));
        }
    }
}

