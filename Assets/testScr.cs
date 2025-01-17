using UnityEngine;
using gameCore;

public class testScr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Weapon weapon = new Weapon(2, 5, true);
        Weapon weapon1 = new Weapon(2, 4, true);
        Item item = new Item(2, 2);
        Item item1 = new Item(1, 1);
         
        Inventory inventory = new Inventory(6, 7);
        inventory.addItem(item);
        inventory.addItem(item);
        inventory.addItem(item);
        inventory.showInventory();
        inventory.addItem(weapon);
        inventory.addItem(weapon);
        inventory.addItem(weapon);
        inventory.addItem(weapon);
        inventory.addItem(weapon1);
        inventory.addItem(item1);
        
        inventory.showInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
