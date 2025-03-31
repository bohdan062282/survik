using UnityEngine;
using UnityEngineInternal;

public class HittingActiveScript : ActiveItemScript
{

    [SerializeField] protected Animator animator;


    protected float _hitDistance = 2.0f;
    protected float _damage = 10.0f;


    public override void initialize(PlayerController playerController, int id)
    {
        _itemID = id;

        _playerController = playerController;

        setOrigin();
    }
    public override void interract()
    {

        animator.SetBool("Attack", true);

    }
    public override void setOrigin()
    {
        transform.SetParent(_playerController.ActiveObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public void onAnimationEnd()
    {

        animator.SetBool("Attack", false);
    }
    public void onAnimationHit()
    {
        hithit();
    }
    //temp
    private void hithit()
    {
        GameObject gameObject =
            _playerController.getFocusObject(_hitDistance, _playerController.hittableLayerMask);

        if (gameObject != null)
        {
            Debug.Log(gameObject.name);

            HittableObject hittableObject = gameObject.GetComponent<HittableObject>();
            if (hittableObject != null) hittableObject.hit(_itemID, _damage);
        }
    }

}
