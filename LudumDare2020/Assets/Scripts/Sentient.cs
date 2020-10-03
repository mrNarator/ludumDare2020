using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    List<IInteractable> InteractablesInRange = new List<IInteractable>();

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
            Debug.Log("added" + inter.ToString());
            InteractablesInRange.Add(inter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var inter = other.GetComponentInParent<IInteractable>();

        if(inter != null)
        {
            Debug.Log("removed" + inter.ToString());
            InteractablesInRange.Remove(inter);
        }
    }


    public void DoFood()
    {
        var food = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Food);
        if(food != null)
        {
            food.Interact(this);
            InteractablesInRange.Remove(food);
        }
    }
    public void ChangeFood(float value) { Food += value; }
    public void DoDrink()
    {
        var drink = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Drink);
        if (drink != null)
        {
            drink.Interact(this);
        }
    }
    public void ChangeDrink(float value) { Drink += value; }

    public void DoStrength()
    {
        var strength = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Strength);
        if (strength != null)
        {
            strength.Interact(this);
        }
    }
    public void ChangeStrength(float value) { Strength += value; }

    public void DoHandsome()
    {
        var handsome = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Handsome);
        if (handsome != null)
        {
            handsome.Interact(this);
        }
    }
    public void ChangeHandsome(float value) { Handsome += value; }

    public void DoLove()
    {
        var love = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Love);
        if (love != null)
        {
            love.Interact(this);
        }
    }
    public void ChangeLove(float value) { Love += value;}

}
