using UnityEngine;
using gameCore;

public class StandingItem : Item
{

    private GameObject _standingObjectPrefab;
    private GameObject _standingObject;
    private GameObject _ghostObjectPrefab;
    private GameObject _ghostObject;


    public StandingItem(int id, GameObject prefab, Sprite iconSprite, string name, ItemRarity rarity, int height, int width, 
                        GameObject activeItemPrefab, GameObject standingObjectPrefab, GameObject ghostObjectPrefab)
            : base(id, prefab, iconSprite, name, rarity, height, width, activeItemPrefab)
    {
        _standingObjectPrefab = standingObjectPrefab;
        _ghostObjectPrefab = ghostObjectPrefab;
    }
    public override void Instantiate(Vector3 position)
    {
        base.Instantiate(position);

        _standingObject = UnityEngine.GameObject.Instantiate(_standingObjectPrefab);
        _standingObject.GetComponent<IItem>().Initialize(this);
        _standingObject.SetActive(false);

        _ghostObject = UnityEngine.GameObject.Instantiate(_ghostObjectPrefab);
        _ghostObject.SetActive(false);

        Outline outlineScr = _standingObject.GetComponent<Outline>();
        if (outlineScr != null) outlineScr.enabled = false;

    }
    public override void take(PlayerController playerController)
    {
        base.take(playerController);

        GhostScript placebleScr2 = _ghostObject.GetComponent<GhostScript>();
        if (placebleScr2 != null) placebleScr2.initialize(playerController);

    }
    public override Item select()
    {
        base.select();

        _ghostObject.SetActive(true);

        return this;
    }
    public override void unSelect()
    {
        base.unSelect();

        _ghostObject.SetActive(false);
    }
    public override void leftMouseClick()
    {
        //be carefull mby add base method
        //base.leftMouseClick();

        _standingObject.SetActive(true);
        
        (Vector3, Vector3) transformParam = _playerController.getPlaceblePosition();

        _standingObject.transform.position = transformParam.Item1;
        _standingObject.transform.up = transformParam.Item2;

    }
    public override void onFocusEnter()
    {
        if (_standingObject.active)
        {
            Outline outlineScr = _standingObject.GetComponent<Outline>();

            if (outlineScr != null)
            {
                outlineScr.enabled = true;
                outlineScr.OutlineColor = Item.rarityOutlineColors[_rarity];
            }
        }
        else base.onFocusEnter();
        
    }
    public override void onFocusExit()
    {
        if (_standingObject.active)
        {
            Outline outlineScr = _standingObject.GetComponent<Outline>();

            if (outlineScr != null)
            {
                outlineScr.enabled = false;
            }
        }
        else base.onFocusExit();
    }

}
