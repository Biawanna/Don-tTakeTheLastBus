using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryScriptableObject inventory;
    public InventoryScriptableObject Inventory { get { return inventory; } }

    public void SetDogBoneTrue()
    {
        inventory.dogBone = true;
    }
    public void SetPopCanTrue()
    {
        inventory.popCan = true;
    }
    public void SetNewspaperTrue()
    {
        inventory.newspaper = true;
    }

    public void SetCoffeeTrue()
    {
        inventory.coffee = true;
    }
    public void SetNewspaperGivenTrue()
    {
        inventory.newspaperGiven = true;
    }
    public void SetCoffeeGiveTrue()
    {
        inventory.coffeeGiven = true;
    }
    public void SetWaterGivenTrue()
    {
        inventory.waterGiven = true;
    }
}
