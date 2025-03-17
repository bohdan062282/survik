using UnityEngine;

public class BarrelGhostScript : MonoBehaviour, IPlaceble
{


    private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            (Vector3, Vector3) transformParam = _playerController.getPlaceblePosition();

            transform.position = transformParam.Item1;
            transform.up = transformParam.Item2;


        }
    }
    public void setPlacingObjPosTransform(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
