using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    public List<ChestController> chestControllers;

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
            int random = Random.Range(1, 4);
            CreateChest((ChestTypes)random);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateChest(ChestTypes types)
    {
        ChestController chestContr = Instantiate(chestPrefab, chestsSpawnLocation.transform);
        chestContr.chestTypes = types;
        chestContr.chestStatus = ChestStatus.Locked;
        chestControllers.Add(chestContr);
    }
}
