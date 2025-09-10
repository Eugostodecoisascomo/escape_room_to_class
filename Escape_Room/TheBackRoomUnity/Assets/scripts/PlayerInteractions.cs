using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
//using FMODUnity;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    public float rayDistance = 4f;
    private Camera myCamera;


    //Input managers
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction clickAction;
    private InputAction clickrightAction;
    float x;
    float y;
    float ck;
    float crk;

    //object move managers
    //float mouseSensibility = 100f;
    public float rotateSpeed = 200f;
    public Transform objectViewer;
    public Transform camera;

    //events managers
    private bool isViewing;
    private InteractableObject currentInteractable;
    private Vector3 originPosition;
    private Quaternion originAngle;
    public UnityEvent OnView;
    public UnityEvent OnFinish; 
    private bool canFinish;

    //[SerializeField]private EventReference inventorySound;

    private PlayerInventory inventory;
    private RopeClimbing ropeClimbing;
    private Item currentItem;


    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
    }
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["MouseMove"];
        clickAction = playerInput.actions["click"];
        clickrightAction = playerInput.actions["clickright"];

        inventory =  GetComponent <PlayerInventory>();
    }

    void OnEnable()
    {
        moveAction.performed += OnMoveEvent;
        clickAction.performed +=OnClickEvent;
        clickrightAction.performed += ClickOutEvent;
    }

    void OnDisable()
    {
        moveAction.performed -= OnMoveEvent;
        clickAction.performed -=OnClickEvent;
        clickrightAction.performed-= ClickOutEvent;
    }


    // Update is called once per frame
    void Update()
    {
        CheckInteractions();
    }

    void CheckInteractions()
    {
        float bmous = ck;
        RaycastHit hit;
        Vector3 rayOrigin = myCamera.ViewportToWorldPoint(new Vector3 (0.5f , 0.5f , 0.5f));

        if(Physics.Raycast(rayOrigin, myCamera.transform.forward, out hit, rayDistance))
        {
            InteractableObject toInteract = hit.collider.GetComponent<InteractableObject>();

            if (toInteract != null)
            {
                UIManager.instance.SetHandCursor(true);
                if (ck != 0)
                {
                    ck = 0f;
                    if (toInteract.isMoving)
                    {
                        return;
                    }
                    currentInteractable = toInteract;
                    
                    if(currentInteractable.item != null)
                    {
                        OnView.Invoke();
                        isViewing = true;
                        bool hasPreviousItem = false;

                        for(int i = 0; i < currentInteractable.previousItems.Length; i++)
                        {
                            if(inventory.itens.Contains(currentInteractable.previousItems[i].requiredItem))
                            {
                                Interact(currentInteractable.previousItems[i].interatctionItem, currentInteractable);
                                currentInteractable.previousItems[i].OnInteract.Invoke();
                                hasPreviousItem = true;
                                break;
                            }
                        }
                        if(hasPreviousItem)
                        {
                            return;
                        }

                        Interact(currentInteractable.item, currentInteractable);
                    }
                    
                    if (currentInteractable.item.grabbable)
                    {
                        /*originPosition = currentInteractable.transform.position;
                        originAngle = currentInteractable.transform.rotation;*/
                        StartCoroutine(MovingObject(currentInteractable, objectViewer.position, currentInteractable.rBody));
                    }

                    if(currentInteractable.item.endTheGame == true)
                    {
                        SceneManager.LoadScene("Screen");
                    }

                    /*if()*/
                }

                if (canFinish && crk !=0)
                {
                    FinishView();
                    crk= 0f;
                }
            }
            else
            {
                UIManager.instance.SetHandCursor(false);
            }
        }
        else if (canFinish && crk !=0)
        {
            FinishView();
            crk= 0f;
        }
        else if(ck != 0)
        {
            ck = 0f;
        }
        else
        {
            UIManager.instance.SetHandCursor(false);
        }

        if(isViewing)
        {
            if(currentInteractable.item.grabbable)
            {
                RotateObject(currentInteractable, objectViewer.position);
            }
            return;
        }
    }

    void Interact(Item itemFake, InteractableObject interactable)
    {
        currentItem = itemFake;
        if(itemFake.image != null)
        {
            UIManager.instance.SetImage(itemFake.image);
        }
        //Audiomanager.instance.PlayOneShot(itemFake.eventReference, interactable.transform.position);
        
        UIManager.instance.SetCaptions(itemFake.text);

        Invoke("CanFinish", 0.1f);
    }

    void CanFinish()
    {
        canFinish = true;
        if (currentItem.image == null && !currentItem.grabbable)
        {
            FinishView();
        }

        UIManager.instance.SetCaptions("");
        print(canFinish);
    }

    void FinishView()
    {
        canFinish = false;
        isViewing = false;
        if (currentItem.canHaveInv)
        {
            inventory.AddItem(currentItem);
            //inventoryAudioSource.Play();
            //Audiomanager.instance.PlayOneShot(inventorySound, transform.position);
            currentInteractable.CollectItem.Invoke();
        }

        if (currentItem.grabbable)
        {
            currentInteractable.transform.rotation = objectViewer.transform.rotation;
            StartCoroutine(MovingObject(currentInteractable, objectViewer.transform.position, currentInteractable.rBody));
        }
        OnFinish.Invoke();
    }

    IEnumerator MovingObject (InteractableObject obj , Vector3 position, Rigidbody nrb)
    {
        obj.isMoving = true;
        obj.transform.position = objectViewer.transform.position;
        obj.transform.rotation = objectViewer.transform.rotation;
        /*float timer = 0f;
        while (timer < 1)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position , position , Time.deltaTime * 5);
            timer = Time.deltaTime;
        }*/
        
        nrb.AddForce(obj.transform.up*10, ForceMode.Impulse);
        nrb.AddForce(obj.transform.forward*4, ForceMode.Impulse);
        yield return null;
        obj.isMoving = false;
    }

    public void RotateObject(InteractableObject obj, Vector3 viwer)
    {
        /*float mx = x * mouseSensibility * Time.deltaTime;
        float my = y * mouseSensibility * Time.deltaTime;
        currentInteractable.transform.Rotate(myCamera.transform.right, -Mathf.Deg2Rad * y * rotateSpeed, Space.World);
        currentInteractable.transform.Rotate(myCamera.transform.up, -Mathf.Deg2Rad * x * rotateSpeed, Space.World);*/
        obj.transform.position = objectViewer.transform.position;
        obj.transform.rotation = objectViewer.transform.rotation;
    }
    
    public void OnMoveEvent(InputAction.CallbackContext value)
    {
        x = value.ReadValue<Vector2>().x;
        y = value.ReadValue<Vector2>().y;
    }
    public void OnClickEvent(InputAction.CallbackContext clicked)
    {
        ck = clicked.ReadValue<float>();
    }
    public void ClickOutEvent(InputAction.CallbackContext riclicked)
    {
        crk = riclicked.ReadValue<float>();
    }

    /*void OnCollisionStay(Collision collider)
    {
        if(collider.other.tag == "Corda")
        {
            toInteract.Item.canClimb = true;
        }
    }*/
}
