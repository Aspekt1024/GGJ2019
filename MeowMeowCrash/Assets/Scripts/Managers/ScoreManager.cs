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

    public float MaxRateOfDecrease = 125f;
    public float ExcitementDecreaseRate = 10f;

    [SerializeField] private float maxExcitement = 1000.0f;
    [SerializeField] private float minRateOfdecrease = 5.0f;
    [SerializeField] private float accelerationOfDecrease = 10.0f;
    [SerializeField] private float amountToReset = 5.0f;

    [SerializeField] private float excitedOnReBat = 5.0f;
    [SerializeField] private float excitementForBat = 100.0f;
    [SerializeField] private float excitementForCollide = 50.0f;
    [SerializeField] private float excitementForFloor = 25.0f;
    [SerializeField] private float excitementOnInitialHit = 350.0f;

    private float currentExcitement = 0.0f;
    private bool excited = false;

    private int score = 0;

    public void checkInitialCollide()
    {
        if(excited == false)
        {
            excited = true;
            currentExcitement += excitementOnInitialHit;
            ExcitementDecreaseRate = minRateOfdecrease;
        }
    }

    private void Start()
    {
        RCStatics.UI.Score.SetScore(score);
        RCStatics.UI.SetExcitement(0);
    }

    void Update()
    {
        if (!excited) return;

        ExcitementDecreaseRate += Time.deltaTime * accelerationOfDecrease;
        ExcitementDecreaseRate = Mathf.Clamp(ExcitementDecreaseRate, minRateOfdecrease, MaxRateOfDecrease);
        currentExcitement -= ExcitementDecreaseRate * Time.deltaTime;
        currentExcitement = Mathf.Clamp(currentExcitement, 0.0f, maxExcitement);

        if (currentExcitement < 0.01f && !RCStatics.Settings.EndlessMode)
        {
            excited = false;
            TransistionController.instance.gameOut();
        }

        RCStatics.UI.SetExcitement(currentExcitement / maxExcitement);
        // TODO play meow sounds based on excitement?
    }

    public void BattedObject()
    {
        checkInitialCollide();
        currentExcitement += excitementForBat;
        ExcitementDecreaseRate -= amountToReset;

        AddToScore((int)excitementForBat);
    }

    public void ReBattedObject()
    {
        checkInitialCollide();
        currentExcitement += excitedOnReBat;

        AddToScore((int)excitedOnReBat);
    }

    public void CollidedObject()
    {
        checkInitialCollide();
        currentExcitement += excitementForBat;
        ExcitementDecreaseRate -= amountToReset;

        AddToScore((int)excitementForBat);
    }

    public void FlooredObject()
    {
        checkInitialCollide();
        currentExcitement += excitementForBat;
        ExcitementDecreaseRate -= amountToReset;

        AddToScore((int)excitementForBat);
    }

    private void AddToScore(int points)
    {
        score += points;
        RCStatics.UI.Score.SetScore(score);
    }
}
