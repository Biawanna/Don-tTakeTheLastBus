using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]

public class InventoryScriptableObject : ScriptableObject
{
    [Header("Invetory Settings")]
    public bool dogTreat;
    public bool headPhones;
    public bool holyWater;
    public int cans = 0;
}

