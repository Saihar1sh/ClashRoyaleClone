using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    public List<ChestController> chestControllers;

    public ChestStatus chestStatus;
    public ChestTypes chestTypes;

    private Button chest;


    protected override void Awake()
    {
        base.Awake();

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
