using UnityEngine;
using gameCore;

public class StandingItem : Item
{
    public static UnityEngine.Color standingOutlineColor = UnityEngine.Color.green;

    private GameObject _placebleObjectPrefab;
    private GameObject _placebleObject;
    private GameObject _standingObjectPrefab;
    private GameObject _standingObject;
    private GameObject _ghostObjectPrefab;
    private GameObject _ghostObject;

    public StandingItem(GameObject prefab, Sprite iconSprite, string name, int height, int width, 
                        GameObject placebleObjectPrefab, GameObject standingObjectPrefab, GameObject ghostObjectPrefab)
            : base(prefab, iconSprite, name, height, width)
    {
        _placebleObjectPrefab = placebleObjectPrefab;
        _standingObjectPrefab = standingObjectPrefab;
        _ghostObjectPrefab = ghostObjectPrefab;
    }
    public override void Instantiate(Vector3 position)
    {
        base.Instantiate(position);

        _placebleObject = UnityEngine.GameObject.Instantiate(_placebleObjectPrefab);
        _placebleObject.SetActive(false);

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

        PlacingScript placebleScr1 = _placebleObject.GetComponent<PlacingScript>();
        if (placebleScr1 != null) placebleScr1.initialize(playerController);
        GhostScript placebleScr2 = _ghostObject.GetComponent<GhostScript>();
        if (placebleScr2 != null) placebleScr2.initialize(playerController);

    }
    public override Item select()
    {
        base.select();

        _placebleObject.SetActive(true);
        _ghostObject.SetActive(true);

        return this;
    }
    public override void unSelect()
    {
        base.unSelect();

        _placebleObject.SetActive(false);
        _ghostObject.SetActive(false);
    }
    public override void interract()
    {
        base.interract();

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
                outlineScr.OutlineColor = standingOutlineColor;
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
