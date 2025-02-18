using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIScript
{
    [SerializeField] public TextMeshProUGUI targetItemText;

    [SerializeField] private Image item1Icon;
    [SerializeField] private Image item2Icon;
    [SerializeField] private Image item3Icon;
    [SerializeField] private Image item4Icon;
    [SerializeField] private Image item5Icon;
    [SerializeField] private Image item6Icon;
    [SerializeField] private Image item7Icon;
    [SerializeField] private Image item8Icon;

    private List<Image> itemIcons;

    public void Initialize()
    {
        itemIcons = new List<Image>() { item1Icon, item2Icon, item3Icon, item4Icon, item5Icon, item6Icon, item7Icon, item8Icon };
    }
    public void setIcon(Texture2D icon, int index)
    {
        if (icon == null)
        {
            
        }
        else
        {

        }
    }
    public void setSelectedIcon(int index)
    {

    }

}
