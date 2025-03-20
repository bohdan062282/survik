using UnityEngine;

public class CampingKnifeActiveScript : ActiveItemScript
{

    private readonly int _id = 0;

    private readonly float _hitDistance = 2.0f;
    private readonly float _damage = 10.0f;



    public override void interract()
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
