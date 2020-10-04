using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Generic;

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

    public override bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove)
    {
        nextMove = AllTypes.GetRandomValue();
        return true;
    }
}
