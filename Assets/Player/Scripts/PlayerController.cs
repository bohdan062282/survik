using UnityEngine;
using UnityEngine.InputSystem;
using gameCore;
using System;


public class PlayerController : MonoBehaviour
{
    
    //ANIMATIONS NEED TO BE CONVERTED TO HASH TO OPTIMIZE!!!
    //NEED TO ADD COMMENTS!!!
    //ADD FALLING STATE

    [Header("Character skills settings")]
    [Space(10)]
    [SerializeField][Range(5.0f, 20.0f)] private float movementSpeed;
    [SerializeField][Range(5.0f, 20.0f)] private float sprintSpeed;
    [SerializeField][Range(10.0f, 300.0f)] private float rotationSpeed;
    [SerializeField][Range(0.0f, 10.0f)] private float interractDistance;
    [Space(10)]

    [Space(10)]
    [Header("References")]
    [Space(10)]
    [SerializeField] private UIScript _UIScript;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Transform cameraTarget;
    [SerializeField] public Transform cameraTransform;
    [SerializeField] public LayerMask itemLayerMask;
    [SerializeField] public Transform PlacebleObjectTransform;
    [SerializeField] private LayerMask groundLayerMask;


    [HideInInspector] public InputAction movementAction;
    [HideInInspector] public InputAction rotationAction;
    [HideInInspector] public InputAction clickAction;
    [HideInInspector] public InputAction interractAction;
    [HideInInspector] public InputAction dropAction;
    [HideInInspector] public InputAction sprintAction;
    [HideInInspector] public InputAction jumpAction;
    //temp
    [HideInInspector] public InputAction item1;
    [HideInInspector] public InputAction item2;
    [HideInInspector] public InputAction item3;
    [HideInInspector] public InputAction item4;
    [HideInInspector] public InputAction item5;
    [HideInInspector] public InputAction item6;
    [HideInInspector] public InputAction item7;
    [HideInInspector] public InputAction item8;


    [HideInInspector] public Vector2 inputVector;

    private Inventory _inventory;
    private Item _focusItem;
    private int _selectedIndex;
    private float _movementSpeed;

    internal StateMachine stateMachine1 { get; private set; }
    internal StateMachine stateMachine2 { get; private set; }

    void Start()
    {
        _inventory = new Inventory(10, 5);
        _focusItem = null;
        _movementSpeed = movementSpeed;

        _UIScript.Initialize();

        movementAction = InputSystem.actions.FindAction("Movement");
        rotationAction = InputSystem.actions.FindAction("Rotation");
        clickAction = InputSystem.actions.FindAction("LMC");
        interractAction = InputSystem.actions.FindAction("Interract");
        dropAction = InputSystem.actions.FindAction("Drop");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");

        //temp
        item1 = InputSystem.actions.FindAction("Item1");
        item2 = InputSystem.actions.FindAction("Item2");
        item3 = InputSystem.actions.FindAction("Item3");
        item4 = InputSystem.actions.FindAction("Item4");
        item5 = InputSystem.actions.FindAction("Item5");
        item6 = InputSystem.actions.FindAction("Item6");
        item7 = InputSystem.actions.FindAction("Item7");
        item8 = InputSystem.actions.FindAction("Item8");


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
        updateFocusItem();


        stateMachine1.Update();
        //stateMachine2.Update();

        processRotation();
        processGravity();

        if (clickAction.WasPerformedThisFrame())
        {
            
        }

        if (sprintAction.IsPressed()) _movementSpeed = sprintSpeed;
        else _movementSpeed = movementSpeed;

        if (jumpAction.WasPerformedThisFrame() && characterController.isGrounded) characterController.Move(new Vector3(0.0f, 3.0f, 0.0f));

        if (interractAction.WasPerformedThisFrame()) processInterractAction();
        if (clickAction.WasPerformedThisFrame()) processItemInterractAction();
        else if (dropAction.WasPerformedThisFrame()) processDropAction();
        else if (item1.WasPerformedThisFrame()) processSelectItemAction(0);
        else if (item2.WasPerformedThisFrame()) processSelectItemAction(1);
        else if (item3.WasPerformedThisFrame()) processSelectItemAction(2);
        else if (item4.WasPerformedThisFrame()) processSelectItemAction(3);
        else if (item5.WasPerformedThisFrame()) processSelectItemAction(4);
        else if (item6.WasPerformedThisFrame()) processSelectItemAction(5);
        else if (item7.WasPerformedThisFrame()) processSelectItemAction(6);
        else if (item8.WasPerformedThisFrame()) processSelectItemAction(7);

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
        characterController.Move(   (transform.forward * inputVector.y * Time.deltaTime * _movementSpeed) + 
                                    (transform.right * inputVector.x * Time.deltaTime * _movementSpeed)      );

    }
    public void processRotation()
    {
        Vector2 vec = rotationAction.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0.0f, vec.x * Time.deltaTime * rotationSpeed, 0.0f));

        cameraTarget.Rotate(new Vector3(-vec.y * Time.deltaTime * rotationSpeed, 0.0f, 0.0f));

    }
    private void processGravity()
    {
        
        if (!characterController.isGrounded) characterController.Move(new Vector3(0.0f, -1.0f, 0.0f) * 10.0f * Time.deltaTime);
        
    }
    private void processSelectItemAction(int index)
    {
        _inventory.unSelectItem();

        _selectedIndex = index;

        _UIScript.setSelectedIcon(index);

        _inventory.selectItem(index, PlacebleObjectTransform);

    }
    private void processInterractAction()
    {
        if (_focusItem != null)
        {
            if (_inventory.addItem(_focusItem))
            {
                _focusItem.take(this);

                _UIScript.updateToolbar(_inventory.Items);
            }
            else
            {
                _focusItem.drop(cameraTarget.transform.position, transform.rotation.eulerAngles.y, cameraTarget.transform.forward * 5.0f);
            }

        } 
    }
    private void processItemInterractAction()
    {
        if ( _inventory.ActiveItem != null)
        {
            if (_inventory.ActiveItem is StandingItem)
            {
                Item item = _inventory.dropActiveItem();
                if (item != null)
                {
                    _UIScript.unSetSelectedIcon();
                    item.unSelect();
                    item.interract();

                    _UIScript.updateToolbar(_inventory.Items);

                }
            }
        }
    }
    private void processDropAction()
    {
        _UIScript.unSetSelectedIcon();



        Item item = _inventory.dropActiveItem();
        if (item != null)
        {
            item.unSelect();
            item.drop(cameraTarget.transform.position, transform.rotation.eulerAngles.y, cameraTarget.transform.forward * 5.0f);
        }

        _UIScript.updateToolbar(_inventory.Items);
    }
    private void updateFocusItem()
    {
        _focusItem = getFocusItem();
        if (_focusItem == null)
        {
            _UIScript.targetItemText.text = "";
        }
        else
        {
            _UIScript.targetItemText.text = _focusItem.getName();
        }
    }
    //get focus Item object
    private Item getFocusItem()
    {
        GameObject gameObject = getFocusItemObject();
        if (gameObject != null)
        {
            if (clickAction.WasPerformedThisFrame()) Debug.Log(gameObject.name);
            GameObject rootObject = gameObject.transform.root.gameObject;
            IItem rootObjectScript = rootObject.GetComponent<IItem>();
            if (rootObjectScript != null)
            {
                Item rootObjectItem = rootObjectScript.getItemObject();
                if (rootObjectItem != null) return rootObjectItem;
                else return null;
            }
            else return null;
        }
        else return null;
    }
    //get object by scope focus using interractDistance
    private GameObject getFocusItemObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interractDistance, itemLayerMask))
            return hit.collider.gameObject;
        else
            return null;
    }
    public (Vector3, Vector3) getPlaceblePosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interractDistance, groundLayerMask))
            return (hit.point, hit.normal);
        else return (cameraTransform.position + (cameraTransform.forward * interractDistance), PlacebleObjectTransform.up);
    }



}
