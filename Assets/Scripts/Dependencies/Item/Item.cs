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
        public static UnityEngine.Color droppedOutlineColor = UnityEngine.Color.yellow;

        private string _name;
        private Vector2Int _size;
        private GameObject _prefab;
        private Sprite _icon;
        private GameObject _droppedGameObject;
        private GameObject _activeItemPrefab;
        private GameObject _activeItemGameObject;
        protected PlayerController _playerController;

        public Item(GameObject prefab, Sprite iconSprite, string name, int height, int width, GameObject activeItemPrefab)
        {
            _name = name;
            _prefab = prefab;
            _icon = iconSprite;
            _size = new Vector2Int(height, width);
            _activeItemPrefab = activeItemPrefab;

            //refactor
            IsDropped = true;
        }
        public virtual void Instantiate(Vector3 position)
        {
            _droppedGameObject = UnityEngine.Object.Instantiate(_prefab, position, Quaternion.identity);
            _droppedGameObject.GetComponent<IItem>().Initialize(this);
            Outline outlineScr = _droppedGameObject.GetComponent<Outline>();
            if (outlineScr != null) outlineScr.enabled = false;
            _activeItemGameObject = UnityEngine.Object.Instantiate(_activeItemPrefab);
            _activeItemGameObject.SetActive(false);

        }
        public virtual void onFocusEnter()
        {
            Outline outlineScr = _droppedGameObject.GetComponent<Outline>();

            if (outlineScr != null)
            {
                outlineScr.enabled = true;
                outlineScr.OutlineColor = droppedOutlineColor;
            }
        }
        public virtual void onFocusExit()
        {
            Outline outlineScr = _droppedGameObject.GetComponent<Outline>();

            if (outlineScr != null)
            {
                outlineScr.enabled = false;
            }
        }
        public virtual void take(PlayerController playerController)
        {
            IsDropped = false;

            _droppedGameObject.SetActive(false);

            _playerController = playerController;

            ActiveItemScript activeItemScript = _activeItemGameObject.GetComponent<ActiveItemScript>();
            if (activeItemScript != null) activeItemScript.initialize(playerController);
        }
        public void drop(Vector3 position, float Yrotation, Vector3 force)
        {
            IsDropped = true;

            _droppedGameObject.SetActive(true);

            _droppedGameObject.transform.position = position;
            _droppedGameObject.transform.rotation = Quaternion.identity;
            _droppedGameObject.transform.Rotate(new Vector3(0.0f, Yrotation, 0.0f));

            Rigidbody rb = _droppedGameObject.GetComponent<Rigidbody>();
            if (rb != null ) rb.AddForce(force, ForceMode.Impulse);
            
        }
        public virtual Item select()
        {
            _activeItemGameObject.SetActive(true);

            return this;
        }
        public virtual void unSelect()
        {
            _activeItemGameObject.SetActive(false);
        }
        public virtual void leftMouseClick()
        {
            ActiveItemScript activeScript = _activeItemGameObject.GetComponent<ActiveItemScript>();
            if (activeScript != null) activeScript.interract();
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
        public bool IsDropped { get; private set; }
        
    }
    public enum ItemState { DROPPED, ININVENTORY, STAND };
}
