using UnityEngine;
using UnityEngine.InputSystem;
using gameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.BoolParameter;
using UnityEngineInternal;


public class PlayerController : MonoBehaviour
{
    
    //ANIMATIONS NEED TO BE CONVERTED TO HASH TO OPTIMIZE!!!
    //NEED TO ADD COMMENTS!!!
    //ADD FALLING STATE

    [Header("Character skills settings")]
    [Space(10)]
    [SerializeField][Range(5.0f, 20.0f)] private float movementSpeed;
    [SerializeField][Range(10.0f, 300.0f)] private float rotationSpeed;
    [Space(10)]

    [Space(10)]
    [Header("References")]
    [Space(10)]
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Transform cameraTarget;
    [SerializeField] public Transform cameraTransform;
    [SerializeField] public LayerMask itemLayerMask;


    [HideInInspector] public InputAction movementAction;
    [HideInInspector] public InputAction rotationAction;
    [HideInInspector] public InputAction clickAction;


    [HideInInspector] public Vector2 inputVector;


    internal StateMachine stateMachine1 { get; private set; }
    internal StateMachine stateMachine2 { get; private set; }

    void Start()
    {

        movementAction = InputSystem.actions.FindAction("Movement");
        rotationAction = InputSystem.actions.FindAction("Rotation");
        clickAction = InputSystem.actions.FindAction("LMC");


        stateMachine1 = new StateMachine(new IState[] { new IdlePlayerState(this), 
                                                        new RunPlayerState(this),
                                                                                    });

        //stateMachine2 = new StateMachine(new IState[] { new DefaultPlayerState(this), });



        stateMachine1.Initialize(stateMachine1.States[StateType.IdlePlayerState]);
        //stateMachine1.Initialize(stateMachine1.States[StateType.DefaultPlayerState]);


    }

    void Update()
    {
        setInputVector();


        stateMachine1.Update();
        //stateMachine2.Update();

        processRotation();
        processGravity();

        if (clickAction.WasPerformedThisFrame())
        {
            GameObject gameObject = getFocusObject();
            if (gameObject != null)
            {
                GameObject rootObject = gameObject.transform.root.gameObject;
                Debug.Log(rootObject.name);
                IItem rootObjectScript = rootObject.GetComponent<IItem>();
                if (rootObjectScript != null)
                {
                    Item rootObjectItem = rootObjectScript.getItemObject();
                    if (rootObjectItem != null)
                    {
                        Debug.Log(rootObjectItem.getName());
                    }
                }
            }
        }


    }
    public bool isWASD() => inputVector.x != 0 || inputVector.y != 0;
    private void setInputVector()
    {
        Vector2 vec = movementAction.ReadValue<Vector2>();
        inputVector = new Vector2(vec.x, vec.y);
    }

    //Mozhe perepisat pokruche!!
    public void processMovement()
    {
        characterController.Move(   (transform.forward * inputVector.y * Time.deltaTime * movementSpeed) + 
                                    (transform.right * inputVector.x * Time.deltaTime * movementSpeed)      );

    }
    public void processRotation()
    {
        Vector2 vec = rotationAction.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0.0f, vec.x * Time.deltaTime * rotationSpeed, 0.0f));

        cameraTarget.Rotate(new Vector3(-vec.y * Time.deltaTime * rotationSpeed, 0.0f, 0.0f));

    }
    private void processGravity()
    {
        
        if (!characterController.isGrounded) characterController.Move(new Vector3(0.0f, -1.0f, 0.0f) * 15.0f * Time.deltaTime);
        
    }
    private GameObject getFocusObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 100.0f, itemLayerMask))
            return hit.collider.gameObject;
        else
            return null;
    }



}
