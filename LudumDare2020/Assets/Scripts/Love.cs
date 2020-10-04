using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using static IInteractable;

public class Love : MonoBehaviour, IInteractable, IInteractableInitializer
{
    public float Value = 1;
    // Start is called before the first frame update
    void Start()
    {
        
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
        gameObject.SetActive(false);
        Destroy(gameObject);
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
