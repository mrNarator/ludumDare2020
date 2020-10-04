using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "MovementStrategyComposer", menuName = "BlobMovement/Composer")]
public class MoveStrategyComposer : AMovementStrategy
{
    [SerializeField]
    [ReorderableList]
    private List<AMovementStrategy> _tryApplySequence;

    public override bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove)
    {
        for(int i = 0; i < _tryApplySequence.Count; i++)
        {
            if(_tryApplySequence[i].GetNextMovement(interactables, myPos, out nextMove))
            {
                return true;
            }
        }

        // failing to find where to move;
        nextMove = Movement.MovementType.Backward;
        return false;
    }
}
