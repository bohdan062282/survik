using UnityEngine;

public class BarrelPlacingScript : MonoBehaviour, IPlaceble
{


    private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
