using UnityEngine;

public class GunActiveScript : ActiveItemScript
{

    protected float _damage = 10.0f;
    protected float _bulletSpeed = 20.0f;
    protected int _magSize = 30;
    protected float _calldown = 0.7f;

    protected bool _canRelease = true;
    protected float _releaseStartTime;

    protected BulletPool bulletPool;


    void Start()
    {

        _releaseStartTime = Time.time - _calldown;

    }

    public override void initialize(PlayerController playerController, int id)
    {
        _itemID = id;

        _playerController = playerController;

        transform.SetParent(playerController.ActiveObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        bulletPool = new BulletPool(_magSize);

    }
    public override void interract()
    {

        if (_releaseStartTime + _calldown < Time.time)
        {
            bulletPool.releaseBullet(_playerController.ActiveObjectTransform.position,
                                    _playerController.ActiveObjectTransform.forward.normalized, _bulletSpeed, _damage, _itemID);

            _releaseStartTime = Time.time;
        }


    }


    public GunMode Mode { get; private set; } = GunMode.SEMI;
    
    public enum GunMode
    {
        AUTO, SEMI
    }

}
