using UnityEngine;
using gameCore;

public class GunItem : Item
{

    public GunItem(     int id, GameObject prefab, Sprite iconSprite, string name, ItemRarity rarity, int height, int width,
                        GameObject activeItemPrefab) : base(id, prefab, iconSprite, name, rarity, height, width, activeItemPrefab)
    {

    }

    public GunActiveScript.GunMode GetGunMode() => _activeItemGameObject.GetComponent<GunActiveScript>().Mode;
    public void SwitchGunMode() => _activeItemGameObject.GetComponent<GunActiveScript>().switchMode();



}
