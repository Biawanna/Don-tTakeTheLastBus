using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for the players inventory.
    /// </summary>
    [SerializeField] private InventoryScriptableObject inventory;
    public InventoryScriptableObject Inventory 
    { 
        get { return inventory; }
        set { inventory = value; }
    }

    // Use a generic method to set a boolean item in the inventory.
    public void SetItemTrue(System.Action<bool> itemSetter, bool set)
    {
        itemSetter(set);
    }

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

    /// <summary>
    /// Checks if the player has won the game.
    /// </summary>
    public bool CheckIfPlayerWins()
    {
        var requiredItems = new[]
        {
        inventory.catPicture,
        inventory.headPhones,
        inventory.coconut,
        inventory.soul,
        inventory.holyWater,
        inventory.coffee,
        inventory.dogBone,
        inventory.popCan,
        inventory.newspaper,
        inventory.coffeeGiven,
        inventory.waterGiven,
        inventory.newspaperGiven
        };

        return requiredItems.All(item => item);
    }

    /// <summary>
    /// Resets the players inventory.
    /// </summary>
    public void ResetInventory()
    {
        inventory.catPicture = false;
        inventory.headPhones = false;
        inventory.coconut = false;
        inventory.soul = false;
        inventory.holyWater = false;
        inventory.coffee = false;
        inventory.dogBone = false;
        inventory.popCan = false;
        inventory.newspaper = false;
        inventory.coffeeGiven = false;
        inventory.waterGiven = false;
        inventory.newspaperGiven = false;
    }
}
