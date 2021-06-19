using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    public List<ChestController> chestControllers;

    [SerializeField]
    private ChestScriptableList chestScriptableList;

    [SerializeField]
    private ChestController chestPrefab;
    [SerializeField]
    private GameObject chestsSpawnLocation;

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
            int random = Random.Range(0, 3);
            CreateChest(chestScriptableList.chests[random]);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
