using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Generic;

[CreateAssetMenu(fileName = "SimpleMovementStrategy", menuName = "BlobMovement/Search Strategy Simple")]
public class SearchSingleMovement : AMovementStrategy
    , ISimpleMover
{
    [SerializeField]
    private InteractionType whatToSearch;

    public InteractionType OptimizeType => whatToSearch;

    public override bool GetNextMovement(List<IInteractable> interactables, Vector3 myPos, out Movement.MovementType nextMove, out Vector3 moveVec)
    {
        var sources = interactables.Where(x => x.GetInteractionType() == whatToSearch);
        if(!sources.Any())
        {
            nextMove = Movement.MovementType.Backward;
            moveVec = Vector3.zero;
            return false;
        }

        // To reduce dirWeighted calculations from O(nlogn) to O(n)
        var cache = sources.Select(inter => (dirWeighted: inter.GetValue() / (inter.GetPosition() - myPos).sqrMagnitude, obj: inter)).ToList();
        cache.OrderByDescending(x => x.dirWeighted);
        var moveTo = cache.First();

        moveVec = moveTo.obj.GetPosition() - myPos;
        nextMove = moveVec.ToMoveDirection();

        return true;
    }
}
