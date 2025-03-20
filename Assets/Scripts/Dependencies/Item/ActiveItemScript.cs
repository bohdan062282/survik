using UnityEngine;

public class ActiveItemScript : MonoBehaviour
{

    protected PlayerController _playerController;

    void Start()
    {
        
    }

    void Update()
    {

        updatePosition();

    }

    protected virtual void updatePosition()
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
    public virtual void interract()
    {

    }
}
