using UnityEngine;

public class PlacingScript : MonoBehaviour
{


    protected PlayerController _playerController;

    void Start()
    {
        
    }

    void Update()
    {

        updatePosition();

    }
    private void updatePosition()
    {
        if (_playerController != null)
        {
            transform.position = _playerController.PlacebleObjectTransform.position;
            transform.rotation = _playerController.PlacebleObjectTransform.rotation;
        }
    }
    public void initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
