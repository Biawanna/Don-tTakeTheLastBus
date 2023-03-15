using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]

public class InventoryScriptableObject : ScriptableObject
{
    [Header("Invetory Settings")]
    public bool headPhones;
    public bool holyWater;
    public bool dogBone;
    public bool soul;
    public bool popCan;
    public bool coconut;
    public bool coffee;
    public bool newspaper;
    public bool catPicture;
    public bool coffeeGiven;
    public bool waterGiven;
    public bool newspaperGiven;
}

