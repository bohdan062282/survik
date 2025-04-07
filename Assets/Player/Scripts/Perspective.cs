using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Perspective : MonoBehaviour
{

    [SerializeField][Range(0.0f, 100.0f)] private float sightTransitionSpeed;

    [SerializeField] private CinemachineCamera firstPersonCamera;
    [SerializeField] private CinemachineCamera thirdPersonCamera;

    [SerializeField] private GameObject[] mesh;

    [SerializeReference] private PlayerController player;

    [SerializeField] private RectTransform sight;

    [SerializeField] private PerspectiveType perspective;

    private Camera _mainCamera;

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
        if (perspective == PerspectiveType.FIRST) switchToThird();
        else switchToFirst();
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
            setSight(hit.point);
        }
        else
        {
            setSight(   firstPersonCamera.gameObject.transform.position + 
                        firstPersonCamera.gameObject.transform.forward.normalized * 21.0f);
        }
    }
    private void setSight(Vector3 position)
    {
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(position);

        Vector2 vec2 = new Vector2(screenPos.x - (Screen.width / 2.0f), screenPos.y - (Screen.height / 2.0f));
        sight.anchoredPosition = Vector2.Lerp(sight.anchoredPosition, vec2, sightTransitionSpeed * Time.deltaTime);
    }


    public enum PerspectiveType { FIRST, THIRD }


}
