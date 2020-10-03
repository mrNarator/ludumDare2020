using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sentient : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var inter= other.GetComponentInParent<IInteractable>();
        
        if(inter != null)
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        var inter = other.GetComponentInParent<IInteractable>();

        if(inter != null)
        {

        }
    }
}
