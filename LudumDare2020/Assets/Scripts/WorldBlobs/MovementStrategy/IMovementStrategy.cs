using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IMovementStrategy
{
    bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove);
}

/// <summary>
/// Because Unity is not very nice, at letting developers use interfaces as serialize fields :/
/// </summary>
public abstract class AMovementStrategy : ScriptableObject
    , IMovementStrategy
{
    public abstract bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove);
}
