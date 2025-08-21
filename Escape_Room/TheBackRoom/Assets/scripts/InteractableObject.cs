using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PreviousItem
{
    public Item requiredItem;
    public Item interatctionItem;
    public UnityEvent OnInteract;
}

public class InteractableObject : MonoBehaviour
{
    [HideInInspector]
    public bool isMoving;
    public Item item;
    public UnityEvent OnInteract;
    public UnityEvent CollectItem;

    public PreviousItem [] previousItems;
}
