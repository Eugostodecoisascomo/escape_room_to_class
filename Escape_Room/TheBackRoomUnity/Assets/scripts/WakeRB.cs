using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WakeRB : MonoBehaviour
{
    public UnityEvent breakDown;
    // Start is called before the first frame update

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
        wallBricks.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
