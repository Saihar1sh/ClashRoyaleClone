using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    //[HideInInspector]
    public List<Button> chestBtns;
    //[HideInInspector]
    public List<ChestController> chestControllers;

    /*    [SerializeField]
        private List<Transform> chestTransforms;
    */
    [SerializeField]
    private ChestController chestPrefab;

    [SerializeField]
    private ChestScriptableList chestScriptableList;

    [SerializeField]
    private Button makeRandomChest, coinsBtn, gemsBtn, closeDialogBoxBtn, skipBtn, addToQueBtn;

    [SerializeField]
    private Transform chestsSpawnLocation;

    [SerializeField]
    private Image dialogBox;

    [SerializeField]
    private TextMeshProUGUI dialogBoxTxt;

    public TextMeshProUGUI coinsTxt, gemsTxt;

    public int coins = 100, gems = 100, chestNum = 0;

    public static bool chestUnlocking = false, timerCompleted = false;

    protected override void Awake()
    {
        base.Awake();
        makeRandomChest.onClick.AddListener(CreateRandomChest);
        coinsBtn.onClick.AddListener(BuyCoins);
        gemsBtn.onClick.AddListener(BuyGems);
        closeDialogBoxBtn.onClick.AddListener(CloseDialogBox);
        CloseDialogBox();
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    private void CreateRandomChest()
    {
        int random = Random.Range(0, 4);
        if (chestNum < 4)
            CreateChest(chestScriptableList.chests[random]);
        else
        {
            Debug.LogWarning("Can't create more");
            return;

        }

    }

    // Update is called once per frame
    void Update()
    {
        CurrenciesUIUpdate();
    }

    private void CurrenciesUIUpdate()
    {
        coinsTxt.text = "" + coins;
        gemsTxt.text = "" + gems;
    }

    private void CreateChest(ChestScriptable chestScriptable)
    {
        ChestController chestContr = Instantiate(chestPrefab, chestsSpawnLocation);
        chestContr.AddDetails(chestScriptable);
        chestNum++;

    }

    private void BuyCoins()
    {
        AddCoins(100);
    }
    private void BuyGems()
    {
        AddGems(20);
    }

    //Public Functions

    public void AddChestToList(ChestController chest)
    {
        chestControllers.Add(chest);

    }
    public void SkipTimerOptions(ChestController chestCont)
    {
        SkipTimerOptionsTxt(chestCont);
        DialogBoxDisplayMessage();
        skipBtn.onClick.AddListener(delegate { SkipTimer(chestCont); });
        skipBtn.gameObject.SetActive(true);
    }
    private void SkipTimer(ChestController chest)
    {
        /*        chest.timerSkipped = true;
                timerRunning = false;
        */
        AddGems((int)-chest.skipCost);
        chest.chestStatus = ChestStatus.Unlocked;
        SkippedTimerTxt();
        DialogBoxDisplayMessage();
        chestUnlocking = false;
    }

    public void AddToQueOptions(ChestController chest)
    {
        AddToQueOptionsTxt(chest);
        DialogBoxDisplayMessage();
        addToQueBtn.onClick.AddListener(delegate { AddChestToList(chest); });
        addToQueBtn.gameObject.SetActive(true);
    }

    public void DialogBoxDisplayMessage()
    {
        dialogBoxTxt.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);
        addToQueBtn.gameObject.SetActive(false);
        skipBtn.gameObject.SetActive(false);

    }
    public void CloseDialogBox()
    {
        dialogBox.gameObject.SetActive(false);
        ResetDialogBoxTxt();
    }

    public void AddCoins(int count)
    {
        coins += count;
    }
    public void AddGems(int count)
    {
        gems += count;
    }

    //options texts
    public void ResetDialogBoxTxt()
    {
        dialogBoxTxt.text = "";
    }
    public void SkipTimerOptionsTxt(ChestController chestCont)
    {
        dialogBoxTxt.text = "Running Timer\nDo you want to skip timer?\nCost : " + chestCont.skipCost;

    }
    public void AddToQueOptionsTxt(ChestController chestCon)
    {
        dialogBoxTxt.text = "A chest is unlocking\nDo you want to add this chest to Que?";
    }
    public void SkippedTimerTxt()
    {
        dialogBoxTxt.text = "Timer skipped.";
    }
    public void AddedToQueTxt()
    {
        dialogBoxTxt.text = "Chest added to que. \nWill start unlocking after the current chest unlocks";

    }
}
