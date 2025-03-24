using UnityEngine;

[CreateAssetMenu(fileName = "ItemPreferences", menuName = "Survik settings/ItemPreferences")]
public class ItemPreferences : ScriptableObject
{
    public int id;
    public string itemName;
    public int height;
    public int width;
    public ItemRarity itemRarity;

    [Space(10)]

    public ItemType itemType;


    


    public enum ItemType { Default, Standing }
    public enum ItemRarity { Red, Green, Blue }
}


