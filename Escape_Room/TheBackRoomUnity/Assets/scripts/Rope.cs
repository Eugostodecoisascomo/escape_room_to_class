using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour, IInteractable, ICollidable
{
    bool isClimbing = false;
    bool canClimb = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pIsClose()
    {
        if(canClimb)
        {
            canClimb =false;
        }
        else
        {
            canClimb=true;
        }
    }

    public void Interacted()
    {
        if(isClimbing)
        {
            isClimbing=false;
        }
        else
        {
            isClimbing = true;
        }
    }

    public void Interact() => Interacted();
    public void Collide() => pIsClose();
    
}
