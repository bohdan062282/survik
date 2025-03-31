using UnityEngine;
using UnityEditor;
using gameCore;

[CustomEditor(typeof(ItemPreferences))]
public class ItemPreferencesEditor : Editor
{
    public override void OnInspectorGUI()
    {

        ItemPreferences itemPreferences = (ItemPreferences)target;

        itemPreferences.id = EditorGUILayout.IntField("Item ID", itemPreferences.id);
        itemPreferences.name = EditorGUILayout.TextField("Name", itemPreferences.name);
        itemPreferences.height = EditorGUILayout.IntField("Inventory height", itemPreferences.height);
        itemPreferences.width = EditorGUILayout.IntField("Inventory width", itemPreferences.width);
        itemPreferences.itemRarity = (ItemRarity)EditorGUILayout.EnumPopup("Item rarity", itemPreferences.itemRarity);

        itemPreferences.itemType = (ItemPreferences.ItemType)EditorGUILayout.EnumPopup("Item type", itemPreferences.itemType);

        if (itemPreferences.itemType == ItemPreferences.ItemType.Standing)
        {
            //pipa
        }


        EditorUtility.SetDirty(itemPreferences);

    }
}
