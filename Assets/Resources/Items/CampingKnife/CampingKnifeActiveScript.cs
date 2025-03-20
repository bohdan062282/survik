using UnityEngine;

public class CampingKnifeActiveScript : ActiveItemScript
{

    private readonly float hitDistance = 2.0f;



    public override void interract()
    {

        GameObject gameObject = 
            _playerController.getFocusObject(hitDistance, _playerController.hittableLayerMask);

        if (gameObject != null) Debug.Log(gameObject.name);

    }

}
