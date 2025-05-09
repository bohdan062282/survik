﻿using System;
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
        public static Dictionary<ItemRarity, UnityEngine.Color> rarityOutlineColors = new Dictionary<ItemRarity, UnityEngine.Color>();

        private int _id;
        private string _name;
        protected ItemRarity _rarity;
        private Vector2Int _size;
        private GameObject _prefab;
        private Sprite _icon;
        private GameObject _droppedGameObject;
        private GameObject _activeItemPrefab;
        protected GameObject _activeItemGameObject;
        protected PlayerController _playerController;

        public Item(int id, GameObject prefab, Sprite iconSprite, string name, ItemRarity rarity, int height, int width, GameObject activeItemPrefab)
        {
            _id = id;
            _name = name;
            _rarity = rarity;
            _prefab = prefab;
            _icon = iconSprite;
            _size = new Vector2Int(height, width);
            _activeItemPrefab = activeItemPrefab;
            State = ItemState.DROPPED;

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
                outlineScr.OutlineColor = rarityOutlineColors[_rarity];
                outlineScr.OutlineWidth = 3.0f;
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
            State = ItemState.ININVENTORY;

            _droppedGameObject.SetActive(false);

            _playerController = playerController;

            ActiveItemScript activeItemScript = _activeItemGameObject.GetComponent<ActiveItemScript>();
            if (activeItemScript != null) activeItemScript.initialize(playerController, _id);
        }
        public void drop(Vector3 position, float Yrotation, Vector3 force)
        {
            State = ItemState.DROPPED;

            _droppedGameObject.SetActive(true);

            _droppedGameObject.transform.position = position;
            _droppedGameObject.transform.rotation = Quaternion.identity;
            _droppedGameObject.transform.Rotate(new Vector3(0.0f, Yrotation, 0.0f));

            Rigidbody rb = _droppedGameObject.GetComponent<Rigidbody>();
            if (rb != null )
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.AddForce(force, ForceMode.Impulse);
            }
            
        }
        public virtual void destroy()
        {
            _droppedGameObject.SetActive(false);
            _activeItemGameObject.SetActive(false);

            GameObject.Destroy(_activeItemGameObject);
            GameObject.Destroy(_droppedGameObject, 1.0f);
        }
        public virtual Item select()
        {
            ActiveItemScript activeItemScript = _activeItemGameObject.GetComponent<ActiveItemScript>();
            activeItemScript.setOrigin();

            _activeItemGameObject.SetActive(true);

            return this;
        }
        public virtual void unSelect()
        {
            _activeItemGameObject.SetActive(false);
        }
        public virtual bool leftMouseClick()
        {
            ActiveItemScript activeScript = _activeItemGameObject.GetComponent<ActiveItemScript>();
            if (activeScript != null) activeScript.interract();

            return true;
        }
        public void rotateSize()
        {
            int temp = _size.x;
            _size.x = _size.y;
            _size.y = temp;
        }
        public string getName() { return _name; }
        public ItemRarity getRarity() => _rarity;
        public Sprite getIcon() { return _icon; }
        public Vector2Int getSize() { return _size; }
        public Vector2Int InventoryPosition { get; set; }
        public bool IsRotated { get; set; } = false;
        public ItemState State { get; protected set; }
        
    }
    public enum ItemState { DROPPED, ININVENTORY, STAND };
    public enum ItemRarity { COMMON, RARE, MYTHICAL, LEGENDARY }
}
