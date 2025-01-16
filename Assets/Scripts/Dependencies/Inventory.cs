using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCore
{
    internal class Inventory
    {
        private static readonly int[] MAIN_SLOT_SIZE = new int[] { 5, 2 };
        private static readonly int[] SECONDARY_SLOT_SIZE = new[] { 4, 2 };
        private static readonly int[] INVENTORY_SIZE = new[] { 6, 10 };

        private bool[,] _slots;

        public Inventory(int height, int width) 
        { 
            Size = new int[] { height, width };
            _slots = new bool[height, width];
        }
        public bool addItem(Item item)
        {
            if (item is IHoldable) return false;
            else
            {
                for (int i = 0; i < Size[0]; i++)
                {
                    for (int j = 0; j < Size[1]; j++)
                    {
                        if (i + item.getSize()[0] > Size[0]) return false;
                        if (j + item.getSize()[1] > Size[1]) j = Size[1];
                        else if (checkSubslots(i, j, item.getSize()[0], item.getSize()[1]))
                        {
                            setSlots(i, j, item.getSize()[0], item.getSize()[1]);
                            return true;
                        }
                    }
                }
                return false;
            }  
        }
        private bool checkSubslots(int iStart, int jStart, int iSize, int jSize)
        {
            for (int i = iStart; i < iStart + iSize; i++)
            {
                for (int j = jStart; j < jStart + jSize; j++)
                {
                    if (_slots[i, j] == true) return false;
                }
            }
            return true;
        }
        private void setSlots(int iStart, int jStart, int iSize, int jSize)
        {
            for (int i = iStart; i < iStart + iSize; i++)
            {
                for (int j = jStart; j < jStart + jSize; j++)
                {
                    _slots[i, j] = true;
                }
            }
        }
        public void showInventory()
        {
            for (int i = 0; i < Size[0]; i++)
            {
                for (int j = 0; j < Size[1]; j ++)
                {
                    if (_slots[i, j] == true) Console.Write(1.ToString());
                    else Console.Write(0.ToString());
                }
                Console.Write('\n');
            }
        }

        public int[] Size { get; private set; }
    }
}
