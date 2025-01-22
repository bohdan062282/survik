using gameCore;
using UnityEngine;

internal class FlaskScript : MonoBehaviour, IItem
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
    public Item getItemObject() { return _item; }
}
