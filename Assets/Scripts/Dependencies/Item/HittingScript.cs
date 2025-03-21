using UnityEngine;
using UnityEngineInternal;

public class HittingScript : ActiveItemScript
{

    [SerializeField] protected Animator animator;


    protected int _id = 0;

    protected float _hitDistance = 2.0f;
    protected float _damage = 10.0f;

    protected bool _canHit;


    public override void initialize(PlayerController playerController)
    {
        _playerController = playerController;

        transform.SetParent(playerController.ActiveObjectTransform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public override void interract()
    {

        _canHit = false;

        animator.SetBool("Attack", true);

    }
    public void onAnimationEnd()
    {
        _canHit = true;

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
            if (hittableObject != null) hittableObject.hit(_id, _damage);
        }
    }

}
