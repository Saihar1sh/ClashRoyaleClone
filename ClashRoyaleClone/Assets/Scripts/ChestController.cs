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

    private void ChestButtonPress()
    {
        ChestStatusCheck();

    }

    private void ChestType()
    {
        ColorBlock colors = chest.colors;

        switch (chestTypes)
        {
            case ChestTypes.Common:
                colors.normalColor = Color.gray;
                coinsToCollect = Random.Range(100, 200);
                gemsToCollect = Random.Range(10, 20);
                timerInMins = 15;
                break;

            case ChestTypes.Rare:
                colors.normalColor = new Color32(128, 0, 128, 255);
                coinsToCollect = Random.Range(300, 500);
                gemsToCollect = Random.Range(20, 40);
                timerInMins = 30;
                break;

            case ChestTypes.Epic:
                colors.normalColor = Color.blue;
                coinsToCollect = Random.Range(600, 800);
                gemsToCollect = Random.Range(45, 60);
                timerInHrs = 1;
                break;

            case ChestTypes.Legendary:
                colors.normalColor = new Color32(255, 215, 0, 255);
                coinsToCollect = Random.Range(1000, 1200);
                gemsToCollect = Random.Range(80, 100);
                timerInHrs = 3;
                break;
        }
        chest.colors = colors;
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
        chestTimerTxt.text = timerInHrs + " hrs " + timerInMins + " mins " + timersInSecs + " secs";
    }


    private void SkipTimer()
    {

    }

    private void CollectTreasure()
    {
    }
    private void EmptySlot()
    {
    }


    IEnumerator OneSecWait()
    {
        yield return new WaitForSeconds(1);
    }

}

