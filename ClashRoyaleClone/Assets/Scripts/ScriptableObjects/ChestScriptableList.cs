using UnityEngine;

[CreateAssetMenu(fileName = "New Chest List", menuName = "Chests List")]
public class ChestScriptableList : ScriptableObject
{
    public ChestScriptable[] chests;
}
