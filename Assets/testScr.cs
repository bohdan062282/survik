using UnityEngine;
using gameCore;

public class testScr : MonoBehaviour
{
    [SerializeField] public Sprite sprite;

    void Start()
    {
        Item.droppedOutlineColor = UnityEngine.Color.magenta;
        StandingItem.standingOutlineColor = UnityEngine.Color.white;

        Item item1 = new StandingItem(  Resources.Load<GameObject>("Items/Prek1/Prek1Dropped"),
                                        Resources.Load<Sprite>("Items/Prek1/Prek1Texture"), "Prekos1", 4, 3,
                                        Resources.Load<GameObject>("Items/Prek1/Prek1Placing"),
                                        Resources.Load<GameObject>("Items/Prek1/Prek1Standing"),
                                        Resources.Load<GameObject>("Items/Prek1/Prek1Ghost"));

        Item item2 = new Item(  Resources.Load<GameObject>("Items/Prek1/Prek1Dropped"),
                                Resources.Load<Sprite>("Items/Prek1/Prek1Texture"), "Prekos2", 4, 3,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item3 = new Item(  Resources.Load<GameObject>("Items/Prek1/Prek1Dropped"), 
                                Resources.Load<Sprite>("Items/Prek1/Prek1Texture"), "Prekos3", 4, 3,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item4 = new Item(  Resources.Load<GameObject>("Items/Flask/Flask"),
                                Resources.Load<Sprite>("Items/Flask/FlaskTexture"), "Flask1", 2, 2,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item5 = new Item(  Resources.Load<GameObject>("Items/Flask/Flask"), 
                                Resources.Load<Sprite>("Items/Flask/FlaskTexture"), "Flask2", 2, 2,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item6 = new Item(Resources.Load<GameObject>("Items/Apple/Apple"),
                                Resources.Load<Sprite>("Items/Apple/AppleTexture"), "Apple1", 1, 1,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item7 = new Item(Resources.Load<GameObject>("Items/Apple/Apple"),
                                Resources.Load<Sprite>("Items/Apple/AppleTexture"), "Apple2", 1, 1,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item8 = new Item(Resources.Load<GameObject>("Items/CampingKnife/CampingKnife"),
                                Resources.Load<Sprite>("Items/CampingKnife/CampingKnifeTexture"), "Knife", 1, 2,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item9 = new Item(Resources.Load<GameObject>("Items/Cement/Cement"),
                                Resources.Load<Sprite>("Items/Cement/CementTexture"), "Cement", 2, 3,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item10 = new Item(Resources.Load<GameObject>("Items/GasCylinder/GasCylinder"),
                                Resources.Load<Sprite>("Items/GasCylinder/GasCylinderTexture"), "GasCylinder", 2, 3,
                                Resources.Load<GameObject>("Items/EmptyActiveItem"));

        Item item11 = new StandingItem(Resources.Load<GameObject>("Items/Barrel/Barrel"),
                                        Resources.Load<Sprite>("Items/Barrel/BarrelTexture"), "Barrel", 3, 5,
                                        Resources.Load<GameObject>("Items/Barrel/BarrelPlacing"),
                                        Resources.Load<GameObject>("Items/Barrel/BarrelStanding"),
                                        Resources.Load<GameObject>("Items/Barrel/BarrelGhost"));

        item1.Instantiate(new Vector3(-6.0f, 1.0f, -4.0f));
        item2.Instantiate(new Vector3(-10.0f, 1.0f, -4.0f));
        item3.Instantiate(new Vector3(-14.0f, 1.0f, -4.0f));
        item4.Instantiate(new Vector3(-16.0f, 1.0f, -4.0f));
        item5.Instantiate(new Vector3(-18.0f, 1.0f, -4.0f));
        item6.Instantiate(new Vector3(-19.0f, 3.0f, -4.0f));
        item7.Instantiate(new Vector3(-19.5f, 2.0f, -4.0f));
        item8.Instantiate(new Vector3(-5.0f, 2.0f, -4.0f));
        item9.Instantiate(new Vector3(-6.0f, 2.0f, -5.0f));
        item10.Instantiate(new Vector3(-7.0f, 2.0f, -5.0f));
        item11.Instantiate(new Vector3(-4.0f, 2.0f, -3.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
