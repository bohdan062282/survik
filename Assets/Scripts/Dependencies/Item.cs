using System;
using System.Collections.Generic;
using System.Drawing;
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
        public void rotateSize()
        {
            int temp = _size[0];
            _size[0] = _size[1];
            _size[1] = temp;
        }
        public int[] getSize() { return _size; }
        public bool IsRotated { get; set; } = false;
        
    }
}
