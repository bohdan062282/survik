using UnityEngine;
using gameCore;
using System;


public class PlayerController : MonoBehaviour
{
    
    //ANIMATIONS NEED TO BE CONVERTED TO HASH TO OPTIMIZE!!!
    //NEED TO ADD COMMENTS!!!
    //ADD FALLING STATE

    [Header("Character skills settings")]
    [Space(10)]
    [SerializeField][Range(5.0f, 20.0f)] public float movementSpeed;
    [SerializeField][Range(5.0f, 20.0f)] public float sprintSpeed;
    [SerializeField][Range(0.0f, 5.0f)] public float jumpHeight;
    [SerializeField][Range(0.0f, 4.0f)] public float itemRotationSpeed;
    [SerializeField][Range(10.0f, 300.0f)] private float rotationSpeed;
    [SerializeField][Range(0.0f, 10.0f)] private float interractDistance;
    [Space(10)]
    [SerializeField] private Color commonOutlineColor;
    [SerializeField] private Color rareOutlineColor;
    [SerializeField] private Color mythicalOutlineColor;
    [SerializeField] private Color legendaryOutlineColor;
    [SerializeField] private Color standingOutlineColor;
    [Space(10)]

    [Space(10)]
    [Header("References")]
    [Space(10)]
    [SerializeField] public UIScript UIScript;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Transform cameraTarget;
    [SerializeField] public Transform cameraTransform;

    [SerializeField] public LayerMask itemLayerMask;
    [SerializeField] public LayerMask hittableLayerMask;

    [SerializeField] public Transform PlacebleObjectTransform;
    [SerializeField] public Transform ActiveObjectTransform;

    [SerializeField] private LayerMask groundLayerMask;

    //public states
    [HideInInspector] internal FallingPlayerState fallingPlayerState;


    [HideInInspector] public readonly float gravity = 9.81f;
    [HideInInspector] public float placingRotationDelta = 0.0f;

    [HideInInspector] public Vector3 velocity = new Vector3(0.0f, -2.0f, 0.0f);

    [HideInInspector] public Vector2 inputVector;

    private Inventory _inventory;
    private Item _focusItem;
    private bool _wasSelectedItemThisFrame;
    

    internal StateMachine stateMachine1 { get; private set; }
    internal StateMachine stateMachine2 { get; private set; }

    void Awake()
    {
        Item.rarityOutlineColors[ItemRarity.COMMON] = commonOutlineColor;
        Item.rarityOutlineColors[ItemRarity.RARE] = rareOutlineColor;
        Item.rarityOutlineColors[ItemRarity.MYTHICAL] = mythicalOutlineColor;
        Item.rarityOutlineColors[ItemRarity.LEGENDARY] = legendaryOutlineColor;

    }
    void Start()
    {
        fallingPlayerState = new FallingPlayerState(this);

        _inventory = new Inventory(10, 5);
        _focusItem = null;
        _wasSelectedItemThisFrame = false;


        Cursor.lockState = CursorLockMode.Locked;

        UIScript.Initialize();


        stateMachine1 = new StateMachine(new IState[] { new IdlePlayerState(this), 
                                                        new RunPlayerState(this),
                                                        fallingPlayerState
                                                                                    });

        stateMachine2 = new StateMachine(new IState[] { new DefaultPlayerState(this),
                                                        new PlacingPlayerState(this),
                                                        new ShootingPlayerState(this)});



        stateMachine1.Initialize(stateMachine1.States[StateType.IdleState]);
        stateMachine2.Initialize(stateMachine2.States[StateType.DefaultState]);


        PlayerActions.item1.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item2.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item3.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item4.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item5.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item6.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item7.performed += (x) => { _wasSelectedItemThisFrame = true; };
        PlayerActions.item8.performed += (x) => { _wasSelectedItemThisFrame = true; };

    }

    void Update()
    {

        setInputVector();


        updateFocusItem();


        stateMachine1.Update();
        stateMachine2.Update();


        processVelocity();



        _wasSelectedItemThisFrame = false;

    }
    public bool isWASD() => inputVector.x != 0 || inputVector.y != 0;
    private void setInputVector()
    {
        Vector2 vec = PlayerActions.movementAction.ReadValue<Vector2>();
        inputVector = new Vector2(vec.x, vec.y);
    }

    public void processMovement(float multiplayer, float forwardForce, float rightForce)
    {
        processMovement(transform.forward, transform.right, multiplayer, forwardForce, rightForce);
    }
    public void processMovement(Vector3 forward, Vector3 right, float multiplayer, float forwardForce, float rightForce)
    {
        characterController.Move(   (forward * forwardForce * Time.deltaTime * multiplayer) +
                                    (right * rightForce * Time.deltaTime * multiplayer));
    }


    public void processRotation()
    {
        Vector2 vec = PlayerActions.rotationAction.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0.0f, vec.x * Time.deltaTime * rotationSpeed, 0.0f));

        cameraTarget.Rotate(new Vector3(-vec.y * Time.deltaTime * rotationSpeed, 0.0f, 0.0f));

    }
    private void processVelocity()
    {

        characterController.Move(velocity * Time.deltaTime);
        
    }
    public bool processSelectItemAction(int index)
    {
        
        UIScript.setSelectedIcon(index);

        return _inventory.selectItem(index, PlacebleObjectTransform);
    }
    public void processInteractAction()
    {
        if (_focusItem != null)
        {
            if (_focusItem.State == ItemState.DROPPED)
            {
                if (_inventory.addItem(_focusItem))
                {
                    _focusItem.take(this);

                    UIScript.updateToolbar(_inventory.Items);
                }
                else
                {
                    _focusItem.drop(cameraTarget.transform.position, transform.rotation.eulerAngles.y, cameraTarget.transform.forward * 5.0f);
                }
            }
            else if (_focusItem.State == ItemState.STAND)
            {
                StandingItem standingItem = _focusItem as StandingItem;
                standingItem.onButton_F_Standing();
            }
        } 
    }
    public void processDropAction()
    {
        UIScript.unSetSelectedIcon();

        Item item = _inventory.dropActiveItem();
        if (item != null)
        {
            item.unSelect();
            item.drop(cameraTarget.transform.position, transform.rotation.eulerAngles.y, cameraTarget.transform.forward * 5.0f);
        }

        UIScript.updateToolbar(_inventory.Items);
    }
    private void updateFocusItem()
    {
        Item newFocusItem;
        newFocusItem = getFocusItem();

        if (newFocusItem == null)
        {
            if (_focusItem != null)
            {
                _focusItem.onFocusExit();
                _focusItem = null;
            }
            UIScript.targetItemText.text = "";
        }
        else
        {
            if (_focusItem != null)
            {
                if (_focusItem != newFocusItem)
                {
                    UIScript.targetItemText.text = newFocusItem.getName();
                    UIScript.targetItemText.color = Item.rarityOutlineColors[newFocusItem.getRarity()];
                    _focusItem.onFocusExit();
                    newFocusItem.onFocusEnter();
                    _focusItem = newFocusItem;
                }
            }
            else
            {
                UIScript.targetItemText.text = newFocusItem.getName();
                UIScript.targetItemText.color = Item.rarityOutlineColors[newFocusItem.getRarity()];
                newFocusItem.onFocusEnter();
                _focusItem = newFocusItem;
            }
        }
    }
    //get focus Item object
    private Item getFocusItem()
    {
        GameObject gameObject = getFocusObject(interractDistance, itemLayerMask);
        if (gameObject != null)
        {
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
    public GameObject getFocusObject(float distance, LayerMask layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, distance, layerMask))
            return hit.collider.gameObject;
        else
            return null;
    }
    public (Vector3, Vector3) getPlaceblePosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interractDistance, groundLayerMask))
            return (hit.point, hit.normal);
        else return (cameraTransform.position + (cameraTransform.forward * interractDistance), transform.up);
    }

    public bool getWasSelectedThisFrame() => _wasSelectedItemThisFrame;
    internal Inventory getInventory() => _inventory;

}
