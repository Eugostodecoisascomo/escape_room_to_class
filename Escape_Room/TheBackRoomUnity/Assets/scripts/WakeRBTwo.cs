using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WakeRBTwo : MonoBehaviour
{
    public UnityEvent breakDown;

    [SerializeField] PlayerInteractions playerInteractions;
    [SerializeField] private PlayerInput playerInput;
    private InputAction clickAction;
    float ck;
    [SerializeField]private InteractableObject interactableObject;

    // Start is called before the first frame update
    void Awake()
    {
        clickAction = playerInput.actions["click"];
    }

    void OnEnable()
    {
        clickAction.performed +=OnClickEvent;
    }

    void OnDisable()
    {
        clickAction.performed -=OnClickEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("machado"))
        {
            breakDown.Invoke();
        }
    }
    void Start()
    {
        Rigidbody wallBricks = GetComponent<Rigidbody>();
        interactableObject = GetComponent<InteractableObject>();
        wallBricks.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInteractions.currentInteractable == interactableObject && ck> 0)
        {
            breakDown.Invoke();
        }
    }
    public void OnClickEvent(InputAction.CallbackContext clicked)
    {
        ck = clicked.ReadValue<float>();
    }
}
