using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace gameCore
{
    internal class Item
    {
        private string _name;
        private int[] _size;
        private GameObject _prefab;

        public Item(GameObject prefab, string name, int height, int width)
        {
            _name = name;
            _prefab = prefab;
            _size = new int[] { height, width };
        }
        public void Instantiate(Vector3 position)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(_prefab, position, Quaternion.identity);
            gameObject.GetComponent<IItem>().Initialize(this);

        }
        public void rotateSize()
        {
            int temp = _size[0];
            _size[0] = _size[1];
            _size[1] = temp;
        }
        public string getName() { return _name; }
        public int[] getSize() { return _size; }
        public bool IsRotated { get; set; } = false;
        
    }
}
