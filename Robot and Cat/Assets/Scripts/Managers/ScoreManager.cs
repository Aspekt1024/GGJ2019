using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RobotCat.Player;
using UnityEngine;

public class ScoreManager
{
    private struct ScoreItem
    {
        public GameObject obj;
        public Vector3 initialPos;
        public int timesTracked;
        public ScoreItem(GameObject gameobj)
        {
            obj = gameobj;
            initialPos = gameobj.transform.position;
            timesTracked = 0;
        }
    }

    public float score;
    private List<ScoreItem> scoreItems;
    public ScoreManager()
    {
        scoreItems = new List<ScoreItem>();
    }
    public void Tick()
    {
        
    }

    public void Track(GameObject gameObject)
    {
        ScoreItem[] items = (ScoreItem[])scoreItems.Where(x => x.obj.Equals(gameObject)).ToArray();
        if (items.Length > 0)
        {
            // already in the list
            items[0].timesTracked++;
        }
        else
        {
            scoreItems.Add(new ScoreItem(gameObject));
            score += 50;
            Debug.Log(score);
        }
    }
}

