using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChestController : MonoBehaviour
{
    public ChestStatus chestStatus;
    public ChestTypes chestTypes;

    private int coinsToCollect = 0, gemsToCollect = 0, timerInMins = 0, timerInHrs = 0, timersInSecs = 0;
    private float secsConvert = -1, minsConvert = 0, skipCost = 0;

    private bool timerStarted = false, timerRunning = false;

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

    // Start is called before the first frame update
    void Start()
    {
        chestStatusTxt.text = "Locked";
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            TimerRunning();
            SkipCostCal();
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
                    Debug.Log("A chest is unlocking");

                break;
            case ChestStatus.Unlocking:
                timerOptions();
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
        secsConvert = timerData.z + timerData.y * 60 + timerData.x * 3600;
        Debug.Log("Timer to " + secsConvert);
        timerStarted = true;
        timerRunning = true;
    }

    private void TimerRunning()
    {
        if (timerRunning && secsConvert > 0)
            StartCoroutine(OneSecWaitDecreament());
        else if (secsConvert == 0)
        {
            ChestService.chestUnlocking = false;
            chestStatus = ChestStatus.Unlocked;
        }
        chestTimerTxt.text = "" + secsConvert;
        minsConvert = secsConvert / 60;
        SkipCostCal();

    }

    private void SkipCostCal()
    {
        skipCost = (int)Mathf.Ceil(minsConvert) / 10;
    }

    private void timerOptions()
    {

    }

    private void CollectTreasure()
    {
        ChestService.Instance.gems += gemsToCollect;
        ChestService.Instance.coins += coinsToCollect;
        Destroy(gameObject);
        
    }


    IEnumerator OneSecWaitDecreament()
    {
        timerRunning = false;
        yield return new WaitForSeconds(1);
        secsConvert--;
        timerRunning = true;

    }

}

