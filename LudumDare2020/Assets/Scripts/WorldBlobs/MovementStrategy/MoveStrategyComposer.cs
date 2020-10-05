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
    , IAwareMover
{
    [SerializeField]
    [ReorderableList]
    protected List<AMovementStrategy> _tryApplySequence;

    public override bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove, out Vector3 moveVec)
    {
        for(int i = 0; i < _tryApplySequence.Count; i++)
        {
            if(_tryApplySequence[i].GetNextMovement(interactables, myPos, out nextMove, out moveVec))
            {
                return true;
            }
        }

        // failing to find where to move;
        nextMove = Movement.MovementType.Backward;
        moveVec = Vector3.zero;
        return false;
    }

    public void SetPriorities(List<InteractionType> priorities)
    {
        foreach(var seq in  _tryApplySequence)
        {
            if(seq is IAwareMover)
            {
                (seq as IAwareMover).SetPriorities(priorities);
            }
        }
    }
}
