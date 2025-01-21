
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace gameCore
{
    internal class Inventory
    {
        private bool[,] _slots;

        public Inventory(int height, int width) 
        { 
            Size = new int[] { height, width };
            _slots = new bool[height, width];
            Items = new List<Item>();
        }
        public bool addItem(Item item)
        {
            if (item is IHoldable)
            {
                IHoldable holdableItem = (IHoldable)item;
                bool emptyPrimary = PrimarySlot == null;
                bool emptySecondary = SecondarySlot == null;

                if (holdableItem.IsPrimary)
                {
                    if (emptyPrimary)
                    {
                        PrimarySlot = holdableItem;
                        return true;
                    }
                    else return addToSlots(item);
                }
                else
                {
                    if (emptySecondary)
                    {
                        SecondarySlot = holdableItem;
                        return true;
                    }                       
                    else if (emptyPrimary)
                    {
                        PrimarySlot = holdableItem;
                        return true;
                    }
                    else return addToSlots(item);
                }
            }
            else
            {
                return addToSlots(item);
            }
        }
        private bool addToSlots(Item item)
        {
            if (tryToAddToSlots(item))
            {
                Items.Add(item);
                return true;
            }
            else if (item.getSize()[0] < 2 && item.getSize()[1] < 2)
                return false;
            else
            {
                item.rotateSize();
                item.IsRotated = true;
                if (tryToAddToSlots(item))
                {
                    Items.Add(item);
                    return true;
                }
                else
                {
                    item.rotateSize();
                    item.IsRotated = false;
                    return false;
                }
            }

        }
        private bool tryToAddToSlots(Item item)
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
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < Size[0]; i++)
            {
                for (int j = 0; j < Size[1]; j ++)
                {
                    if (_slots[i, j] == true) sb.Append(1.ToString() + '\t');
                    else sb.Append(0.ToString() + '\t');
                }
                sb.Append("\n\n\n");
            }
            Debug.Log(sb.ToString());
        }

        public IHoldable PrimarySlot { get; set; }
        public IHoldable SecondarySlot { get; set; }
        public List<Item> Items { get; private set; }
        public int[] Size { get; private set; }
    }
}
