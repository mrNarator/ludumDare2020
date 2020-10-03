using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sentient : MonoBehaviour
{
    public float maxHandsome = 5;
    public float maxStrength = 5;
    public float maxFood = 5;
    public float maxDrink = 5;
    public float maxLove = 5;

    public float Handsome { get; private set; } 
    public float Strength { get; private set; }
    public float Food { get; private set; }
    public float Drink { get; private set; }
    public float Love { get; private set; }

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


    public void DoFood()
    {

    }
    public void DoDrink()
    {

    }
    public void DoStrength()
    {

    }
    public void DoHandsome()
    {

    }
    public void DoLove()
    {

    }
}
