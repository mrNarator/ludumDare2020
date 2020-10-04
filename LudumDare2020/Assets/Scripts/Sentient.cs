using Events;
using Generic;
using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sentient : MonoBehaviour
{
    public float maxHandsome = 5;
    public float maxStrength = 5;
    public float maxFood = 5;
    public float maxDrink = 5;
    public float maxLove = 5;

    [ShowNativeProperty]
    public float Handsome { get; private set; }
    [ShowNativeProperty]
    public float Strength { get; private set; }
    [ShowNativeProperty]
    public float Food { get; private set; }
    [ShowNativeProperty]
    public float Drink { get; private set; }
    [ShowNativeProperty]
    public float Love { get; private set; }

    List<IInteractable> InteractablesInRange = new List<IInteractable>();

    public bool InInteractionRange => InteractablesInRange.Count > 0;

    private System.IDisposable evtStream;
    private IPrioritiesEvaluator priorityEvaluator;

    private static List<InteractionType> defaultInteractionSequence = new List<InteractionType>
    {
        InteractionType.Food,
        InteractionType.Drink,
        InteractionType.Handsome,
        InteractionType.Strength,
        InteractionType.Love,
    };

    [UsedImplicitly]
    private void Awake()
    {
        evtStream = MessageBroker.Default.Receive<InteractableConsumedEvt>().Subscribe(evt => TryRemoveOthers(evt.Obj));
        priorityEvaluator = GetComponent<IPrioritiesEvaluator>();
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
        evtStream.Dispose();
    }

    [UsedImplicitly]
    void Update()
    {
        ChangeDrink(-SettingsProvider.Instance.Global.DrinkDecaySpeed * Time.deltaTime);
    }

    [UsedImplicitly]
    private void OnTriggerEnter(Collider other)
    {
        var isVision = other.gameObject.layer == LayerMask.NameToLayer("BlobVision");
        if (isVision) return;

        var inter = other.GetComponentsInParent<IInteractable>();

        if (inter.Length > 0)
        {
            Debug.Log($"added {other.gameObject.name} to {name}");
            InteractablesInRange.AddRange(inter);
        }
    }

    [UsedImplicitly]
    private void OnTriggerExit(Collider other)
    {
        TryRemoveOthers(other.gameObject);
    }

    private void TryRemoveOthers(GameObject obj)
    {
        var isVision = obj.layer == LayerMask.NameToLayer("BlobVision");
        if (isVision) return;

        var inter = obj.GetComponentsInParent<IInteractable>();
        if(inter.Length == 0)
        {
            inter = obj.GetComponents<IInteractable>();
        }

        if (inter.Length > 0)
        {
            InteractablesInRange.RemoveRange(inter);
            Debug.Log($"removed {obj.name} from {name} left: {InteractablesInRange.Count}");
        }
    }


    public void RequestInterection(Sentient OtherSentient)
    {

    }

    public void Interact()
    {
        var priorities = priorityEvaluator?.GetTargetPriorities() ?? defaultInteractionSequence;
        foreach(var pri in priorities)
        {
            if(pri == InteractionType.Food && DoFood())
            {
                UnityEngine.Debug.Log($"<color=#22aa33>{name} do {nameof(DoFood)} - ok</color>");
                return;
            }
            if (pri == InteractionType.Drink && DoDrink()) {
                UnityEngine.Debug.Log($"<color=#22aa33>{name} do {nameof(DoDrink)} - ok</color>");
                return;
            }
            if(pri == InteractionType.Handsome && DoHandsome())
            {
                UnityEngine.Debug.Log($"<color=#22aa33>{name} do {nameof(DoHandsome)} - ok</color>");
                return;
            }
            if(pri == InteractionType.Strength && DoStrength())
            {
                UnityEngine.Debug.Log($"<color=#22aa33>{name} do {nameof(DoStrength)} - ok</color>");
                return;
            }
            if(pri == InteractionType.Love && DoLove())
            {
                UnityEngine.Debug.Log($"<color=#22aa33>{name} do {nameof(DoLove)} - ok</color>");
                return;
            }
        }
    }

    public bool DoFood()
    {
        var food = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Food && x?.IsConsumed == false);
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
        var drink = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Drink && x?.IsConsumed == false);
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
        var strength = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Strength && x?.IsConsumed == false);
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
        var handsome = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Handsome && x?.IsConsumed == false);
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
        var love = InteractablesInRange.FirstOrDefault(x => x?.GetInteractionType() == InteractionType.Love && x?.IsConsumed == false);
        if (love != null)
        {
            love.Interact(this); 

            InteractablesInRange.Remove(love);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeLove(float value) { Love += value;}
}
