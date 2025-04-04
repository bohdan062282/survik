using System;
using Unity.Cinemachine;
using UnityEngine;

public class Perspective : MonoBehaviour
{

    [SerializeField] private CinemachineCamera firstPersonCamera;
    [SerializeField] private CinemachineCamera thirdPersonCamera;

    [SerializeField] private RectTransform sight;

    [SerializeField] private PerspectiveType perspective;


    private Vector2 _cameraDelta;

    void Start()
    {

        if (perspective == PerspectiveType.FIRST) switchToFirst();
        else switchToThird();

    }
    void Update()
    {
        
        if (perspective == PerspectiveType.THIRD)
        {

        }

    }
    public void switchPerspective()
    {
        if (perspective == PerspectiveType.FIRST)
        {
            switchToThird();
        }
        else
        {
            switchToFirst();
        }
    }
    private void switchToFirst()
    {
        perspective = PerspectiveType.FIRST;

        thirdPersonCamera.gameObject.SetActive(false);

        sight.anchoredPosition = Vector2.zero;
    }
    private void switchToThird()
    {
        perspective = PerspectiveType.THIRD;

        thirdPersonCamera.gameObject.SetActive(true);

    }

    public enum PerspectiveType { FIRST, THIRD }


}
