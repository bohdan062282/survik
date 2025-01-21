using UnityEngine;
using gameCore;

public class testScr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Item item1 = new Item(Resources.Load<GameObject>("Items/Prek1"), "Prekos", 4, 3);
        item1.Instantiate(new Vector3(-6.0f, 1.0f, -4.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
