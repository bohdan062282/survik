using UnityEngine;

public class ActiveItemScript : MonoBehaviour
{

    public PlayerController _playerController;
    protected int _itemID;

    void Start()
    {
        
    }

    void Update()
    {


    }

    public virtual void initialize(PlayerController playerController, int id)
    {
        _itemID = id;

        _playerController = playerController;

        setOrigin();
    }
    public virtual void interract()
    {

    }
    public virtual void setOrigin()
    {
        transform.SetParent(_playerController.PlacebleObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

}
