namespace gameCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory(5, 7);
            Item i1 = new Item(2, 3);

            inventory.addItem(i1);
            inventory.addItem(i1);
            
            Weapon w = new Weapon(2, 5);

            inventory.addItem(w);

            inventory.showInventory();

        }
    }
}
