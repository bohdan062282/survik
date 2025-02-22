using UnityEngine;
using gameCore;

public class StandingItem : Item
{
    private GameObject _placebleObjectPrefab;
    private GameObject _placebleObject;
    private GameObject _standingObjectPrefab;
    private GameObject _standingObject;

    public StandingItem(GameObject prefab, Sprite iconSprite, string name, int height, int width, 
                        GameObject placebleObjectPrefab, GameObject standingObjectPrefab)
            : base(prefab, iconSprite, name, height, width)
    {
        _placebleObjectPrefab = placebleObjectPrefab;
        _standingObjectPrefab = standingObjectPrefab;
    }
    public override void Instantiate(Vector3 position)
    {
        base.Instantiate(position);
        _placebleObject = UnityEngine.GameObject.Instantiate(_placebleObjectPrefab);
        _placebleObject.SetActive(false);
        _standingObject = UnityEngine.GameObject.Instantiate(_standingObjectPrefab);
        _standingObject.SetActive(false);

    }
    public override void take(PlayerController playerController)
    {
        base.take(playerController);

        IPlaceble placebleScr = _placebleObject.GetComponent<IPlaceble>();
        if (placebleScr != null) placebleScr.setPlacingObjPosTransform(playerController);

    }
    public override Item select()
    {
        base.select();

        _placebleObject.SetActive(true);

        return this;
    }
    public override void unSelect()
    {
        base.unSelect();

        _placebleObject.SetActive(false);
    }

}
