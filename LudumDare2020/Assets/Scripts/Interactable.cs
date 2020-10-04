using UnityEngine;

public interface IInteractable
{
    void Interact(Sentient Interactor);

    float GetValue();

    InteractionType GetInteractionType();

    Vector3 GetPosition();
}

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
