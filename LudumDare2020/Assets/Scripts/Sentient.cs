using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sentient : MonoBehaviour
{
    public float maxStrength = 5;
    public float maxFood = 5;
    public float maxDrink = 5;
    public float maxLove = 5;


    float Strength = 1;
    float Food = 1;
    float Drink = 1;
    float Love = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
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
