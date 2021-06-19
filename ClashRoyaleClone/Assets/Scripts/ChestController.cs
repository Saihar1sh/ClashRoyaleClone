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

    private bool timerStarted = false, hrsdecrease = false, minsDecrease = false, secsDecrease = false;

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
        ChestType();

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

    private void ChestType()
    {
        ColorBlock colors = chest.colors;

    }
    private void ChestStatusCheck()
    {
        switch (chestStatus)
        {
            case ChestStatus.Locked:
                StartTimer();
                break;
            case ChestStatus.Unlocking:
                SkipTimer();
                break;
            case ChestStatus.Unlocked:
                CollectTreasure();
                break;
            case ChestStatus.Collected:
                EmptySlot();
                break;
        }
    }
    private void StartTimer()
    {
        chestStatus = ChestStatus.Unlocking;
        chestStatusTxt.text = "Unlocking";
        chestTimerTxt.text = timerData.x + " hrs " + timerData.y + " mins " + timerData.z + " secs";
        //float timerinSecs = timerData.z + timerData.y * 60 + timerData.x * 3600;
        //Debug.Log("Timer to " + timerinSecs);
        if (timerData.x <= 0)
        {
            timerData.x = 00;
        }
        else
        {
            // timerData
        }
        if (timerData.y <= 0)
        {
            timerData.y = 00;
        }
    }


    private void SkipTimer()
    {

    }

    private void CollectTreasure()
    {
    }
    private void EmptySlot()
    {
        gameObject.SetActive(false);
    }


    IEnumerator OneSecWaitDecreament(int count)
    {
        yield return new WaitForSeconds(1);

    }

}

