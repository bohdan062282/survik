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

            transform.forward = _playerController.transform.forward;

            transform.up = transformParam.Item2;

            transform.Rotate(new Vector3(0.0f, _playerController.placingRotationDelta, 0.0f));

        }
    }
    public void initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
