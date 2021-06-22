using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChestController : MonoBehaviour
{
    //[HideInInspector]
    public ChestStatus chestStatus;
    //[HideInInspector]
    public ChestTypes chestTypes;

    private int coinsToCollect = 0, gemsToCollect = 0, timerInMins = 0, timerInHrs = 0, timersInSecs = 0;
    private float secsConvert = -1, skipConvert = 0;

    public float skipCost = 0;

    private bool timerStarted = false, timerRunning = false, timerSkipped = false;

    [Tooltip("Hrs, Mins, Secs")]
    private Vector3 timerData;                                                  //x- hrs, y- mins, z- secs

    private Button chest;

    [SerializeField]
    private TextMeshProUGUI chestStatusTxt, chestTimerTxt;

    private void Awake()
    {
        chest = GetComponent<Button>();
        chest.onClick.AddListener(ChestButtonPress);

    }

    void Start()
    {
        timerSkipped = false;
        chestStatus = ChestStatus.Locked;
        chestStatusTxt.text = "Locked";
        secsConvert = timerData.z + timerData.y * 60 + timerData.x * 3600;
        chestTimerTxt.text = "" + secsConvert;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            SkipCostCal();
            TimerRunning();
        }
    }

    public void AddDetails(ChestScriptable chestScriptable)
    {
        chestStatus = ChestStatus.Locked;
        chestTypes = chestScriptable.chestType;
        timerData = chestScriptable.Timer;
        coinsToCollect = Random.Range(chestScriptable.coinsMin, chestScriptable.coinsMax + 1);
        gemsToCollect = Random.Range(chestScriptable.gemMin, chestScriptable.gemMax + 1);
        ColorBlock colors = chest.colors;
        Color _color = chestScriptable.Color;
        colors.normalColor = _color;
        _color.a = 0.9f;
        colors.highlightedColor = _color;
        _color.a = 0.75f;
        colors.pressedColor = _color;
        chest.colors = colors;
        gameObject.SetActive(true);
    }

    private void ChestButtonPress()
    {
        ChestStatusCheck();

    }

    private void ChestStatusCheck()
    {
        switch (chestStatus)
        {
            case ChestStatus.Locked:
                if (!ChestService.chestUnlocking)
                    StartTimer();
                else
                    ChestService.Instance.AddToQueOptions(this);

                break;
            case ChestStatus.Unlocking:
                ChestService.Instance.SkipTimerOptions(this);
                break;
            case ChestStatus.Unlocked:
                CollectTreasure();
                break;
            case ChestStatus.Collected:
                break;
        }
    }
    private void StartTimer()
    {
        ChestService.chestUnlocking = true;
        chestStatus = ChestStatus.Unlocking;
        chestStatusTxt.text = "Unlocking";
        timerStarted = true;
        timerRunning = true;
    }

    private void TimerRunning()
    {
        if (timerRunning && secsConvert > 0 && !timerSkipped)
            StartCoroutine(OneSecWaitDecreament());
        else if (secsConvert == 0 || timerSkipped)
        {
            TimerCompletedRun();
        }
        chestTimerTxt.text = "" + secsConvert;
        skipConvert = secsConvert / 10;

    }

    private void TimerCompletedRun()
    {
        secsConvert = 0;
        ChestService.chestUnlocking = false;
        chestStatus = ChestStatus.Unlocked;
        chestStatusTxt.text = "Unlocked";
        ChestService.timerCompleted = true;
        timerRunning = false;
    }

    private void SkipCostCal()
    {
        skipCost = (int)Mathf.Ceil(skipConvert);
    }



    private void CollectTreasure()
    {
        ChestService.Instance.AddGems(gemsToCollect);
        ChestService.Instance.AddCoins(coinsToCollect);
        Destroy(gameObject);
        ChestService.Instance.chestNum--;
    }


    IEnumerator OneSecWaitDecreament()
    {
        timerRunning = false;
        yield return new WaitForSeconds(1);
        secsConvert--;
        timerRunning = true;

    }

}

