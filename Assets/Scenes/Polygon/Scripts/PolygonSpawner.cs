using UnityEngine;
using gameCore;
using System.Collections.Generic;

public class PolygonSpawner : MonoBehaviour
{


    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private ItemsPreferences itemsPreferences;


    void Start()
    {

        spawnItems(spawnPoints, itemsPreferences);
        
    }

    void Update()
    {
        
    }

    private void spawnItems(Transform[] spawners, ItemsPreferences itemsPreferences)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawnItem(spawners[i], itemsPreferences.getItems()[i]);
        }
    }
    private void spawnItem(Transform transform, ItemPreferences itemPreferences)
    {
        Item item;

        if (itemPreferences.itemType == ItemPreferences.ItemType.Default)
        {
            string name = itemPreferences.name;

            item = new Item(    itemPreferences.id,
                                Resources.Load<GameObject>("Items/" + name + "/" + name),
                                Resources.Load<Sprite>("Items/" + name + "/" + name + "Texture"), name, itemPreferences.height, itemPreferences.width,
                                Resources.Load<GameObject>("Items/" + name + "/" + name + "Active"));

            item.Instantiate(transform.position);
        }
        else if (itemPreferences.itemType == ItemPreferences.ItemType.Standing)
        {
            string name = itemPreferences.name;

            item = new StandingItem(    itemPreferences.id,
                                        Resources.Load<GameObject>("Items/" + name + "/" + name),
                                        Resources.Load<Sprite>("Items/" + name + "/" + name + "Texture"), name, itemPreferences.height, itemPreferences.width,
                                        Resources.Load<GameObject>("Items/" + name + "/" + name + "Placing"),
                                        Resources.Load<GameObject>("Items/" + name + "/" + name + "Standing"),
                                        Resources.Load<GameObject>("Items/" + name + "/" + name + "Ghost"));

            item.Instantiate(transform.position);
        }
        else if (itemPreferences.itemType == ItemPreferences.ItemType.Gun)
        {
            string name = itemPreferences.name;

            item = new GunItem( itemPreferences.id,
                                Resources.Load<GameObject>("Items/" + name + "/" + name),
                                Resources.Load<Sprite>("Items/" + name + "/" + name + "Texture"), name, itemPreferences.height, itemPreferences.width,
                                Resources.Load<GameObject>("Items/" + name + "/" + name + "Active"));

            item.Instantiate(transform.position);
        }

        
    }
}
