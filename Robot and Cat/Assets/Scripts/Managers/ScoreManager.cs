using System.Collections.Generic;
using System.Linq;
using RobotCat;
using UnityEngine;

public class ScoreManager:MonoBehaviour
{

    public static ScoreManager instance = null;

    private void Awake()
    {
       if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public float maxExcitement = 1000.0f;
    public float currentExcitement = 0.0f;
    public float excitementForCollide = 50.0f;
    public float excitementForBat = 100.0f;
    public float excitementForFloor = 25.0f;
    public float excitementOnInitialHit = 350.0f;
    public float excitementDecreaseRate = 5.0f;
    public float maxRateOfDecrease = 100.0f;
    public float minRateOfdecrease = 5.0f;
    public float excitedOnReBat = 5.0f;
    public float accelerationOfDecrease = 10.0f;
    private bool excited = false;
    public float amountToReset = 5.0f;



    public void checkInitialCollide()
    {
        if(excited == false)
        {
            excited = true;
            currentExcitement += excitementOnInitialHit;
            excitementDecreaseRate = minRateOfdecrease;
        }
    }


    void Update()
    {
        if(excited)
        {
            excitementDecreaseRate += Time.deltaTime * accelerationOfDecrease;
            excitementDecreaseRate = Mathf.Clamp(excitementDecreaseRate, minRateOfdecrease, maxRateOfDecrease);
            currentExcitement -= excitementDecreaseRate * Time.deltaTime;
            currentExcitement = Mathf.Clamp(currentExcitement, 0.0f, maxExcitement);
            if (currentExcitement < 0.01f)
            {
                excited = false;
                TransistionController.instance.gameOut();
            }

        }

        RCStatics.UI.SetExcitement(currentExcitement / maxExcitement);
    }

    public void battedObject()
    {
        checkInitialCollide();
        currentExcitement += excitementForBat;
        excitementDecreaseRate -= amountToReset;
    }

    public void reBattedObject()
    {
        checkInitialCollide();
        currentExcitement += excitedOnReBat;
    }

    public void collidedObject()
    {
        checkInitialCollide();
        currentExcitement += excitementForCollide;
        excitementDecreaseRate -= amountToReset;
    }

    public void flooredObject()
    {
        checkInitialCollide();
        currentExcitement += excitementForFloor;
        excitementDecreaseRate -= amountToReset;
    }
    



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

