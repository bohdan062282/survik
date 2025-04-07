using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using gameCore;
using Unity.VisualScripting;

[Serializable]
public class UIScript
{
    [SerializeField] private TextMeshProUGUI targetItemText;

    [SerializeField] private Image item1Icon;
    [SerializeField] private Image item2Icon;
    [SerializeField] private Image item3Icon;
    [SerializeField] private Image item4Icon;
    [SerializeField] private Image item5Icon;
    [SerializeField] private Image item6Icon;
    [SerializeField] private Image item7Icon;
    [SerializeField] private Image item8Icon;

    [SerializeField] private Image selectedIcon;


    private List<Image> _itemIcons;

    public void Initialize()
    {
        _itemIcons = new List<Image>() { item1Icon, item2Icon, item3Icon, item4Icon, item5Icon, item6Icon, item7Icon, item8Icon };
    }
    private void setIcon(Sprite itemSprite, int index)
    {
        if (itemSprite == null)
        {
            _itemIcons[index].sprite = null;
            _itemIcons[index].color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            _itemIcons[index].sprite = itemSprite;
            _itemIcons[index].color = Color.white;
        }
    }
    public void unSetSelectedIcon()
    {
        selectedIcon.gameObject.SetActive(false);
    }
    public void setSelectedIcon(int index)
    {
        selectedIcon.gameObject.SetActive(true);
        float xPos = (index * 64) + (index * 8);
        selectedIcon.rectTransform.anchoredPosition = new Vector3(xPos, 0.0f, 0.0f);
    }
    public void updateToolbar(List<Item> items)
    {
        for (int i = 0; i < _itemIcons.Count; i++)
        {
            if (i < items.Count) setIcon(items[i].getIcon(), i);
            else setIcon(null, i);
        }
    }
    public void updateFocusText(Item item)
    {
        targetItemText.text = item.getName();
        targetItemText.color = Item.rarityOutlineColors[item.getRarity()];
    }
    public void resetFocusText() => targetItemText.text = "";
    public int getToolbarSize() => _itemIcons.Count;
}
