using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Perspective : MonoBehaviour
{

    public GameObject sphere;

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
        RaycastHit hit;
        if (Physics.Raycast(firstPersonCamera.gameObject.transform.position,
                            firstPersonCamera.gameObject.transform.forward, out hit, 20.0f))
        {
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(hit.point);
            Resolution resolution = Screen.currentResolution;

            Vector2 vec = new Vector2(screenPos.x - (resolution.width / 2.0f), screenPos.y - (resolution.height / 2.0f));
            sight.anchoredPosition = vec;
            Debug.Log(vec);
            sphere.transform.position = hit.point;
        }
        else
        {
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(firstPersonCamera.gameObject.transform.position + firstPersonCamera.gameObject.transform.forward.normalized * 21.0f);
            Resolution resolution = Screen.currentResolution;

            Vector2 vec = new Vector2(screenPos.x - (resolution.width / 2.0f), screenPos.y - (resolution.height / 2.0f));
            sight.anchoredPosition = vec;
            Debug.Log(vec);
            sphere.transform.position = hit.point;
        }
    }


    public enum PerspectiveType { FIRST, THIRD }


}
