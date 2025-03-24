using UnityEngine;

public class GunActiveScript : ActiveItemScript
{

    protected int _id = 0;

    protected float _damage = 10.0f;


    public override void initialize(PlayerController playerController)
    {
        _playerController = playerController;

        transform.SetParent(playerController.ActiveObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

}
