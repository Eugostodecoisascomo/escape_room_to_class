using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using FMODUnity;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    //[SerializeField] public EventReference eventReference;
    public bool requiredItem;
    public bool grabbable;
    public bool endTheGame;
    

    public string text;

    //public AudioClip audioClip;

    public Sprite image;

    [Header("Inventory")]
    public bool canHaveInv;
    public string collectMessage;
}
