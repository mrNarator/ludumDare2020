using UnityEngine;

public interface IInteractable
{
    void Interact(Sentient Interactor);

    float GetValue();

    bool IsConsumed { get; }

    InteractionType GetInteractionType();

    Vector3 GetPosition();
}

public interface IFood : IInteractable { }
public interface ILove : IInteractable { }

public interface IInteractableInitializer
{
    void Init(float initialValue);
}

public enum InteractionType
{
    Food,
    Drink,
    Handsome,
    Love,
    Strength
}
