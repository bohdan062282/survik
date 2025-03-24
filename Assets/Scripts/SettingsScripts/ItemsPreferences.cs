using UnityEngine;

[CreateAssetMenu(fileName = "ItemsPreferences", menuName = "Survik settings/Items Preferences")]
public class ItemsPreferences : ScriptableObject
{

    [SerializeField]
    private ItemPreferences[] items;


    public ItemPreferences[] getItems() => items;


}
