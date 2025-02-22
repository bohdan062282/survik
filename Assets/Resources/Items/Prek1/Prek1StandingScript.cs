using UnityEngine;
using gameCore;

public class Prek1StandingScript : MonoBehaviour, IItem
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
    public Item getItemObject() => _item;
}
