using UnityEngine;
using gameCore;

public class DroppedItemScript : MonoBehaviour, IItem
{


    private Item _item;


    void Start()
    {

    }
    void Update()
    {

    }
    public void Initialize(Item itemObject)
    {
        _item = itemObject;
    }
    public Item getItemObject() { return _item; }

}
