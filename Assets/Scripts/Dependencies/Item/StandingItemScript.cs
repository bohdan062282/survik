using gameCore;
using UnityEngine;

public class StandingItemScript : MonoBehaviour, IItem
{


    private Item _item;


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
    public virtual void onInterract()
    {
        Debug.Log("Interracted with " + _item.getName());
    }
    public Item getItemObject() => _item;
}
