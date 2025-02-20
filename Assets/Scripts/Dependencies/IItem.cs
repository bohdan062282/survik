

namespace gameCore
{
    internal interface IItem
    {
        public Item getItemObject();
        public void Initialize(Item itemObject);

        //need to write drop() func for Inventory
    }
}
