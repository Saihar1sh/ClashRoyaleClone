using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestController : MonoBehaviour
{
    public ChestStatus chestStatus;
    public ChestTypes chestTypes;

    private Button chest;
    private TextMeshProUGUI chestStatusTxt;

    private void Awake()
    {
        chest = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChestService.Instance.chestControllers.Add(this);
        ChestType();
    }

    // Update is called once per frame
    void Update()
    {

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
    }
    private void ChestStatusCheck()
    {
        switch (chestStatus)
        {
            case ChestStatus.Locked:
                chestStatus = ChestStatus.Unlocking;
                break;
            case ChestStatus.Unlocking:
                break;
            case ChestStatus.Unlocked:
                break;
            case ChestStatus.Collected:
                break;
        }
    }

}

