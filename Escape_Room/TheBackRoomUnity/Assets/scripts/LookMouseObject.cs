using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookMouseObject : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerInteractions playerInteractions;
    [SerializeField] private GameObject oProprio;
    //private PlayerInput playerInput;
    private InputAction mouseAction;
    private Camera myCamera;
    public float rotateSpeed = 200f;


    private float mouseX;
    private float mouseY;
    public float mouseSensibility = 50.0f;
    private float Xrotation;
    //public Transform playerBody;


    void Awake()
    {
        mouseAction = playerInput.actions["MouseMove"];
    }

    void OnEnable()
    {
        mouseAction.performed += OnMouseEvent;
        mouseAction.canceled += MouseCancel;
    }

    void OnDisable()
    {
        mouseAction.performed -= OnMouseEvent;
        mouseAction.performed -= MouseCancel;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mX = mouseX * mouseSensibility * Time.deltaTime;
        float mY = mouseY * mouseSensibility * Time.deltaTime;
        oProprio.transform.Rotate(myCamera.transform.right, -Mathf.Deg2Rad * mY * rotateSpeed, Space.World);
        oProprio.transform.Rotate(myCamera.transform.up, -Mathf.Deg2Rad * mX * rotateSpeed, Space.World);

        Xrotation = mY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
        //playerBody.Rotate(Vector3.up * mX);
    }
    public void OnMouseEvent (InputAction.CallbackContext value)
    {
        mouseX = value.ReadValue<Vector2>().x ;
        mouseY = value.ReadValue<Vector2>().y ;
    }

    public void MouseCancel (InputAction.CallbackContext value)
    {
        mouseX = 0f;
        mouseY = 0f;
    }
}
