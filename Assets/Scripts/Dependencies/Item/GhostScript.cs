using UnityEngine;

public class GhostScript : MonoBehaviour
{


    protected PlayerController _playerController;

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
    public void initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
