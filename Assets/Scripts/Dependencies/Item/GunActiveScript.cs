using UnityEngine;

public class GunActiveScript : ActiveItemScript
{

    protected int _id = 0;

    protected float _damage = 10.0f;
    protected float _bulletSpeed = 20.0f;
    protected int _magSize = 30;

    protected BulletPool bulletPool;


    public override void initialize(PlayerController playerController)
    {
        _playerController = playerController;

        transform.SetParent(playerController.ActiveObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        bulletPool = new BulletPool(_magSize);

    }
    public override void interract()
    {

        bulletPool.releaseBullet(   _playerController.ActiveObjectTransform.position, 
                                    _playerController.ActiveObjectTransform.forward.normalized, _bulletSpeed, _damage, _id);


    }



}
