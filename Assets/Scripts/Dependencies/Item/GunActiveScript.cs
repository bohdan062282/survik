using UnityEngine;

public class GunActiveScript : ActiveItemScript
{

    [SerializeField] protected float damage = 10.0f;
    [SerializeField] protected float bulletSpeed = 20.0f;
    [SerializeField] protected int magSize = 30;
    [SerializeField] protected float calldown = 0.5f;

    protected bool _canSwitchMode = true;

    protected float _releaseStartTime;

    protected BulletPool bulletPool;


    void Start()
    {

        _releaseStartTime = Time.time - calldown;

    }

    public override void initialize(PlayerController playerController, int id)
    {
        _itemID = id;

        _playerController = playerController;

        setOrigin();

        bulletPool = new BulletPool(magSize);

    }
    public override void interract()
    {

        if (_releaseStartTime + calldown < Time.time)
        {
            bulletPool.releaseBullet(_playerController.ActiveObjectTransform.position,
                                    _playerController.ActiveObjectTransform.forward.normalized, bulletSpeed, damage, _itemID);

            _releaseStartTime = Time.time;
        }

    }
    public override void setOrigin()
    {
        transform.SetParent(_playerController.ActiveObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public void switchMode()
    {
        if (_canSwitchMode)
        {
            if (Mode == GunMode.AUTO)
                Mode = GunMode.SEMI;
            else
                Mode = GunMode.AUTO;
        }
    }
           


    public GunMode Mode { get; private set; } = GunMode.SEMI;
    
    public enum GunMode
    {
        AUTO, SEMI
    }

}
