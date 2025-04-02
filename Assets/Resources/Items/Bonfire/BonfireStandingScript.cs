using UnityEngine;

public class BonfireStandingScript : StandingItemScript
{

    [SerializeField] private GameObject fireObject;


    public override void onInterract()
    {
        base.onInterract();

        if (fireObject.active) fireObject.SetActive(false);
        else fireObject.SetActive(true);
    }


}
