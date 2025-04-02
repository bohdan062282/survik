using UnityEngine;
using gameCore;

public class Prek1StandingScript : StandingItemScript
{

    private int _rotationMultiplayer = 1;

    void Update()
    {
        transform.Rotate(0.0f, 20.0f * Time.deltaTime * _rotationMultiplayer, 0.0f);
    }
    public override void onInterract()
    {
        base.onInterract();

        _rotationMultiplayer *= -1;
    }

}
