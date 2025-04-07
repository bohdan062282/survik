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
    [Space(10)]

    [Space(10)]
    [Header("Scripts")]
    [Space(10)]
    [SerializeField] private Perspective perspective;
    [SerializeField] public UIScript UIScript;

    [Space(10)]
    [Header("References")]
    [Space(10)]
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Animator animator;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] public Transform PlacebleObjectTransform;
    [SerializeField] public Transform ActiveObjectTransform;

    [SerializeField] private LayerMask itemLayerMask;
    [SerializeField] public LayerMask hittableLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    [HideInInspector] public readonly float gravity = 9.81f;

    [HideInInspector] public float placingRotationDelta = 0.0f;
    [HideInInspector] public Vector3 velocity = new Vector3(0.0f, -2.0f, 0.0f);
    [HideInInspector] public Vector2 inputVector;

    [HideInInspector] internal FallingPlayerState fallingPlayerState;

    private Inventory _inventory;
    private HealthSystem _healthSystem;

    private Item _focusItem;
    private bool _wasSelectedItemThisFrame;
    private (Vector3 position, Vector3 normal) _placebleLastTransform;
    

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
        _healthSystem = new HealthSystem();
        _focusItem = null;
        _wasSelectedItemThisFrame = false;
        _placebleLastTransform = new (Vector3.zero, Vector3.zero);


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
        Debug.Log(characterController.isGrounded);

        setInputVector();


        updateFocusItem();


        stateMachine1.Update();
        stateMachine2.Update();


        processVelocity();


        if (PlayerActions.perspectiveChange.WasPerformedThisFrame()) perspective.switchPerspective();


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
    public void useConsumable(ConsumingActiveScript.ConsumingParams consumingParams)
    {
        _healthSystem.affectConsumable(consumingParams);

        Item item = processDropAction();

        if (item != null) item.destroy();
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
    public Item processDropAction()
    {
        Item item;

        UIScript.unSetSelectedIcon();

        item = _inventory.dropActiveItem();
        if (item != null)
        {
            item.unSelect();
            item.drop(cameraTarget.transform.position, transform.rotation.eulerAngles.y, cameraTarget.transform.forward * 5.0f);
        }

        UIScript.updateToolbar(_inventory.Items);

        return item;
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
            UIScript.resetFocusText();
        }
        else
        {
            if (_focusItem != null)
            {
                if (_focusItem != newFocusItem)
                {
                    UIScript.updateFocusText(newFocusItem);
                    _focusItem.onFocusExit();
                    newFocusItem.onFocusEnter();
                    _focusItem = newFocusItem;
                }
            }
            else
            {
                UIScript.updateFocusText(newFocusItem);
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
        {
            _placebleLastTransform.position = Vector3.Lerp(_placebleLastTransform.position, hit.point, 30.0f * Time.deltaTime);
            _placebleLastTransform.normal = Vector3.Lerp(_placebleLastTransform.normal, hit.normal, 30.0f * Time.deltaTime);
        }
        else
        {
            _placebleLastTransform = (cameraTransform.position + (cameraTransform.forward * interractDistance), transform.up);
        }
        return _placebleLastTransform;
    }

    public bool getWasSelectedThisFrame() => _wasSelectedItemThisFrame;
    internal Inventory getInventory() => _inventory;

}
