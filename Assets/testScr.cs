using UnityEngine;
using gameCore;

public class testScr : MonoBehaviour
{
    [SerializeField] public Sprite sprite;

    void Start()
    {
        Item item1 = new Item(  Resources.Load<GameObject>("Items/Prek1/Prek1Dropped"),
                                Resources.Load<Sprite>("Items/Prek1/Prek1Texture"), "Prekos1", 4, 3);
        Item item2 = new Item(  Resources.Load<GameObject>("Items/Prek1/Prek1Dropped"),
                                Resources.Load<Sprite>("Items/Prek1/Prek1Texture"), "Prekos2", 4, 3);
        Item item3 = new Item(  Resources.Load<GameObject>("Items/Prek1/Prek1Dropped"), 
                                Resources.Load<Sprite>("Items/Prek1/Prek1Texture"), "Prekos3", 4, 3);
        Item item4 = new Item(  Resources.Load<GameObject>("Items/Flask/Flask"),
                                Resources.Load<Sprite>("Items/Flask/FlaskTexture"), "Flask1", 2, 2);
        Item item5 = new Item(  Resources.Load<GameObject>("Items/Flask/Flask"), 
                                Resources.Load<Sprite>("Items/Flask/FlaskTexture"), "Flask2", 2, 2);
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
