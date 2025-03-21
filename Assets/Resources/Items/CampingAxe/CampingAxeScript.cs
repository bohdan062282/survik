using UnityEngine;
using gameCore;

public class CampingAxeScript : MonoBehaviour, IItem
{
    private Item _item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize(Item itemObject)
    {
        _item = itemObject;
    }
    public Item getItemObject() { return _item; }
}
