using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using static IInteractable;

[RequireComponent(typeof(InteractableConsume))]
public class Love : MonoBehaviour, IInteractable, IInteractableInitializer
{
    public float Value = 1;
    private InteractableConsume consumeRef;
    public bool IsConsumed => consumeRef.Consumed;
    // Start is called before the first frame update
    void Awake()
    {
        consumeRef = GetComponent<InteractableConsume>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InteractionType GetInteractionType()
    {
        return InteractionType.Love;
    }

    public float GetValue()
    {
        return Value;
    }

    public void Interact(Sentient Source)
    {
        Source.ChangeLove(Value);
        consumeRef.MarkConsumed();
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void Init(float initialValue)
    {
        Value = initialValue;
    }
}
