using gameCore;
using UnityEngine;

public class ArmyPistolScript : MonoBehaviour, IItem
{

    private Item _item;




    public void Initialize(Item itemObject)
    {
        _item = itemObject;
    }
    public Item getItemObject() { return _item; }
}

