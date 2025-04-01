using UnityEngine;
using gameCore;

public class Prek1StandingScript : StandingItemScript
{

    void Update()
    {
        transform.Rotate(0.0f, 20.0f * Time.deltaTime, 0.0f);
    }

}
