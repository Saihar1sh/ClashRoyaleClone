using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    public List<ChestController> chestControllers;

    [SerializeField]
    private ChestScriptableList chestScriptableList;

    [SerializeField]
    private ChestController chestPrefab;
    [SerializeField]
    private GameObject chestsSpawnLocation;

    public TextMeshProUGUI coinsTxt, gemsTxt;

    public int coins = 100, gems = 100;

    public static bool chestUnlocking = false;

    protected override void Awake()
    {
        base.Awake();

    }


    // Start is called before the first frame update
    void Start()
    {
        CreateRandomChest();
    }

    private void CreateRandomChest()
    {
        for (int i = 0; i < 4; i++)
        {
            int random = Random.Range(0, 4);
            CreateChest(chestScriptableList.chests[random]);
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
        ChestController chestContr = Instantiate(chestPrefab, chestsSpawnLocation.transform);
        chestContr.AddDetails(chestScriptable);
    }

    private void AddChestToList(ChestController chest)
    {
        chestControllers.Add(chest);
    }
}
