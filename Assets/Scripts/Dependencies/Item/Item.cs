using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace gameCore
{
    public class Item
    {
        private string _name;
        private Vector2Int _size;
        private GameObject _prefab;
        private Sprite _icon;
        private GameObject _gameObject;

        public Item(GameObject prefab, Sprite iconSprite, string name, int height, int width)
        {
            _name = name;
            _prefab = prefab;
            _icon = iconSprite;
            _size = new Vector2Int(height, width);
        }
        public virtual void Instantiate(Vector3 position)
        {
            _gameObject = UnityEngine.Object.Instantiate(_prefab, position, Quaternion.identity);
            _gameObject.GetComponent<IItem>().Initialize(this);

        }
        public virtual void take()
        {
            _gameObject.SetActive(false);
        }
        public void drop(Vector3 position, float Yrotation, Vector3 force)
        {
            _gameObject.SetActive(true);
            _gameObject.transform.position = position;
            _gameObject.transform.rotation = Quaternion.identity;
            _gameObject.transform.Rotate(new Vector3(0.0f, Yrotation, 0.0f));
            Rigidbody rb = _gameObject.GetComponent<Rigidbody>();
            if (rb != null )
            {
                rb.AddForce(force, ForceMode.Impulse);
            }
            
        }
        public void rotateSize()
        {
            int temp = _size.x;
            _size.x = _size.y;
            _size.y = temp;
        }
        public string getName() { return _name; }
        public Sprite getIcon() { return _icon; }
        public Vector2Int getSize() { return _size; }
        public Vector2Int InventoryPosition { get; set; }
        public bool IsRotated { get; set; } = false;
        
    }
    public enum ItemState { DROPPED, ININVENTORY, STAND };
}
