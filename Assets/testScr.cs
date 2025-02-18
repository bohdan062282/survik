using UnityEngine;
using gameCore;

public class testScr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Item item1 = new Item(Resources.Load<GameObject>("Items/Prek1/Prek1"), "Prekos1", 4, 3, ItemState.DROPPED);
        Item item2 = new Item(Resources.Load<GameObject>("Items/Prek1/Prek1"), "Prekos2", 4, 3, ItemState.DROPPED);
        Item item3 = new Item(Resources.Load<GameObject>("Items/Prek1/Prek1"), "Prekos3", 4, 3, ItemState.DROPPED);
        Item item4 = new Item(Resources.Load<GameObject>("Items/Flask/Flask"), "Flask1", 2, 2, ItemState.DROPPED);
        Item item5 = new Item(Resources.Load<GameObject>("Items/Flask/Flask"), "Flask2", 2, 2, ItemState.DROPPED);
        item1.Instantiate(new Vector3(-6.0f, 1.0f, -4.0f));
        item2.Instantiate(new Vector3(-10.0f, 1.0f, -4.0f));
        item3.Instantiate(new Vector3(-14.0f, 1.0f, -4.0f));
        item4.Instantiate(new Vector3(-16.0f, 1.0f, -4.0f));
        item5.Instantiate(new Vector3(-18.0f, 1.0f, -4.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
