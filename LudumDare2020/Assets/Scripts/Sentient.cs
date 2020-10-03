using JetBrains.Annotations;
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

    [UsedImplicitly]
    void Update()
    {
        ChangeDrink(-SettingsProvider.Instance.Global.DrinkDecaySpeed * Time.deltaTime);
    }

    [UsedImplicitly]
    private void OnTriggerEnter(Collider other)
    {
        var inter = other.GetComponentInParent<IInteractable>();

        if (inter != null)
        {
            Debug.Log("added" + inter.ToString());
            InteractablesInRange.Add(inter);
        }
    }

    [UsedImplicitly]
    private void OnTriggerExit(Collider other)
    {
        var inter = other.GetComponentInParent<IInteractable>();

        if (inter != null)
        {
            Debug.Log("removed" + inter.ToString());
            InteractablesInRange.Remove(inter);
        }
    }


    public void RequestInterection(Sentient OtherSentient)
    {

    }
    public void Interact()
    {
        if (DoFood()) {  
        }
        else if (DoDrink()) { 
        }
        else if (DoHandsome()){
        }
        else if (DoStrength()){      
        }
        else
        {

        }
    }

    public bool DoFood()
    {
        var food = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Food);
        if(food != null)
        {
            food.Interact(this);
            InteractablesInRange.Remove(food);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeFood(float value) { Food += value; }
    public bool DoDrink()
    {
        var drink = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Drink);
        if (drink != null)
        {
            drink.Interact(this);
            InteractablesInRange.Remove(drink);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeDrink(float value) { Drink += value; }

    public bool DoStrength()
    {
        var strength = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Strength);
        if (strength != null)
        {
            strength.Interact(this);
            InteractablesInRange.Remove(strength);

            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeStrength(float value) { Strength += value; }

    public bool DoHandsome()
    {
        var handsome = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Handsome);
        if (handsome != null)
        {
            handsome.Interact(this);

            InteractablesInRange.Remove(handsome);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeHandsome(float value) { Handsome += value; }

    public bool DoLove()
    {
        var love = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Love);
        if (love != null)
        {
            love.Interact(this); 

            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeLove(float value) { Love += value;}

}
