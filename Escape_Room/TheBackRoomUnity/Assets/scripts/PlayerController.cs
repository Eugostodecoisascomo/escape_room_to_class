using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LookMouseCamera lookMouseCamera;
    [SerializeField] private Rigidbody rigidBody;

    public CharacterController controller;
    private float x;
    private float z;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float maxSpeed;
    Vector3 velocity;
    Vector3 move;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public bool canClimb = false;
    bool isClimbing = false;

    private PlayerInput playerInput;
    private InputAction moveAction;


    // Start is called before the first frame update
    void Start()
    {}

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    void OnEnable()
    {
        moveAction.performed += OnMoveEvent;
        moveAction.canceled += CancelMove;
       
    }

    void OnDisable()
    {
        moveAction.performed -= OnMoveEvent;
        moveAction.canceled -= CancelMove;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(!isClimbing)
        {
            move = transform.right * x + transform.forward * z;
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(move * speed * Time.deltaTime);

        

        controller.Move(velocity * Time.deltaTime);
    }

    void OnCollisionEnter( Collision collider)
    {
        if(collider.other.tag == "Corda")
        {
            canClimb = true;
        }
    }

    void OnCollisionExit( Collision collider)
    {
        canClimb = false;
    }

    public void OnMoveEvent(InputAction.CallbackContext value)
    {
        x = value.ReadValue<Vector2>().x;
        z = value.ReadValue<Vector2>().y;
    }

    public void CancelMove(InputAction.CallbackContext value)
    {
        x = 0;
        z = 0;
    }
}
