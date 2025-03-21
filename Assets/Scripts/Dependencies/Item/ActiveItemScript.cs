using UnityEngine;

public class ActiveItemScript : MonoBehaviour
{

    protected PlayerController _playerController;

    void Start()
    {
        
    }

    void Update()
    {


    }

    public virtual void initialize(PlayerController playerController)
    {
        _playerController = playerController;

        transform.SetParent(playerController.PlacebleObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public virtual void interract()
    {

    }
}
