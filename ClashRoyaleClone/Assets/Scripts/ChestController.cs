using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChestController : MonoBehaviour
{
    public ChestStatus chestStatus;
    public ChestTypes chestTypes;

    private Button chest;
    [SerializeField]
    private TextMeshProUGUI chestStatusTxt;

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
                break;
            case ChestTypes.Rare:
                colors.normalColor = new Color32(128, 0, 128, 255);
                break;
            case ChestTypes.Epic:
                colors.normalColor = Color.blue;
                break;
            case ChestTypes.Legendary:
                colors.normalColor = new Color32(255, 215, 0, 255);
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

    private void EmptySlot()
    {
        throw new NotImplementedException();
    }

    private void SkipTimer()
    {

        throw new NotImplementedException();
    }

    private void CollectTreasure()
    {
        throw new NotImplementedException();
    }

    private void StartTimer()
    {
        chestStatus = ChestStatus.Unlocking;
        chestStatusTxt.text = "Unlocking";
        throw new NotImplementedException();
    }
}

