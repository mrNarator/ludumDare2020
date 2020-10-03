using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Generic;

public interface IMovementStrategy
{
    Movement.MovementType GetNextMovement();
}

/// <summary>
/// Because Unity is not very nice, at letting developers use interfaces as serialize fields :/
/// </summary>
public abstract class AMovementStrategy : ScriptableObject
    , IMovementStrategy
{
    public abstract Movement.MovementType GetNextMovement();
}

[CreateAssetMenu(fileName = "RandomMovementStrategy", menuName = "BlobMovement/Random")]
public class RandomMovement : AMovementStrategy
{
    private List<Movement.MovementType> _allTypes;
    private List<Movement.MovementType> AllTypes
    {
        get {
            if(_allTypes?.Any() != true)
            {
                _allTypes = Extensions.GetAllValues<Movement.MovementType>();
            }
            return _allTypes;
        }
    }

    public override Movement.MovementType GetNextMovement()
    {
        return AllTypes.GetRandomValue();
    }
}
