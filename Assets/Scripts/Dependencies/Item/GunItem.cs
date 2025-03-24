using UnityEngine;
using gameCore;

public class GunItem : Item
{

    public GunItem(     GameObject prefab, Sprite iconSprite, string name, int height, int width,
                        GameObject activeItemPrefab) : base(prefab, iconSprite, name, height, width, activeItemPrefab)
    {

    }
    
}
