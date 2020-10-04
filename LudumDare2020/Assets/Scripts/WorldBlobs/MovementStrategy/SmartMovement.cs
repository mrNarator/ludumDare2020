using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Generic;

[CreateAssetMenu(fileName = "SmartMovementStrategy", menuName = "BlobMovement/Search Needed Strategy")]
public class SmartMovement : MoveStrategyComposer
    , IAwareMover
{
    private List<InteractionType> Priorities { get; set; }
    private List<ISimpleMover> OptimeMoverStrategies { get; set; }

    public override bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove)
    {
        foreach(var pri in Priorities)
        {
            foreach (var strat in OptimeMoverStrategies)
            {
                if (strat.OptimizeType == pri && strat.GetNextMovement(interactables, myPos, out nextMove))
                {
                    return true;
                }
            }
        }

        nextMove = Movement.MovementType.Backward;
        return false;
    }

    public void SetPriorities(List<InteractionType> priorities)
    {
        Priorities = priorities;
    }

    private bool Validate()
    {
        var ifaceType = typeof(ISimpleMover);
        return _tryApplySequence.All(x => ifaceType.IsAssignableFrom(x.GetType()));
    }

    private void OnEnable()
    {
        if(Validate())
        {
            OptimeMoverStrategies = _tryApplySequence.Cast<ISimpleMover>().ToList();
        }
        else
        {
            throw new TypeAccessException($"Not all assigned strategies implement {nameof(ISimpleMover)} interface");
        }
    }
}
