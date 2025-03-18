using UnityEngine;

public class PlacingScript : ActiveItemScript
{

    void Start()
    {
        
    }

    void Update()
    {

        updatePosition();

    }
    protected override void updatePosition()
    {
        if (_playerController != null)
        {
            transform.position = _playerController.PlacebleObjectTransform.position;
            transform.rotation = _playerController.PlacebleObjectTransform.rotation;
        }
    }

}
