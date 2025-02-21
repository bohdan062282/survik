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
        _standingObject = UnityEngine.GameObject.Instantiate(_standingObject);
        _standingObject.SetActive(false);

    }
    public override void take()
    {
        base.take();
    }

}
