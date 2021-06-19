using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chests")]
public class ChestScriptable : ScriptableObject
{
    public ChestTypes chestType;
    //public ChestStatus chestStatus;

    public int coinsMin;
    public int coinsMax;

    public int gemMin;
    public int gemMax;

    [Tooltip("x- Hrs, y- Mins, z- Secs")]
    public Vector3 Timer;

    public Color Color;
}
