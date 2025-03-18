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
        
    }
    public void initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
