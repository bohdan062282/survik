using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Perspective : MonoBehaviour
{

    [SerializeField] private CinemachineCamera firstPersonCamera;
    [SerializeField] private CinemachineCamera thirdPersonCamera;

    //mesh
    [SerializeField] private GameObject[] mesh;

    [SerializeReference] private PlayerController player;
    [SerializeField] private RectTransform sight;

    [SerializeField] private PerspectiveType perspective;

    private Camera _mainCamera;
    private Vector2 _cameraDelta;

    void Start()
    {

        _mainCamera = Camera.main;

        if (perspective == PerspectiveType.FIRST) switchToFirst();
        else switchToThird();

    }
    void Update()
    {
        
        if (perspective == PerspectiveType.THIRD)
        {
            processSight();
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

        foreach (GameObject go in mesh)
        {
            go.SetActive(false);
        }

        sight.anchoredPosition = Vector2.zero;
    }
    private void switchToThird()
    {
        perspective = PerspectiveType.THIRD;

        thirdPersonCamera.gameObject.SetActive(true);

        foreach (GameObject go in mesh)
        {
            go.SetActive(true);
        }

    }
    private void processSight()
    {
        
    }


    public enum PerspectiveType { FIRST, THIRD }


}
