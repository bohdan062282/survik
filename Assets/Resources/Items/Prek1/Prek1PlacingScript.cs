using UnityEngine;

public class Prek1PlacingScript : MonoBehaviour, IPlaceble
{

    private PlayerController _playerController;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        updatePosition();

    }
    public void updatePosition()
    {
        if (_playerController != null)
        {
            transform.position = _playerController.PlacebleObjectTransform.position;
            transform.rotation = _playerController.PlacebleObjectTransform.rotation;
        }
    }
    public void setPlacingObjPosTransform(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
