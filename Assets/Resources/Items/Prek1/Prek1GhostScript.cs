using UnityEngine;

public class Prek1GhostScript : MonoBehaviour, IPlaceble
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
            transform.position = _playerController.PlacebleObjectTransform.position + (_playerController.transform.forward * 3.0f);
            Debug.Log(_playerController.transform.forward);
        }
    }
    public void setPlacingObjPosTransform(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
