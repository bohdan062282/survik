using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCore
{
    internal class Item
    {
        private int[] _size;

        public Item(int height, int width)
        {
            _size = new int[] { height, width };
        }
        public int[] getSize() { return _size; }

        
    }
}
