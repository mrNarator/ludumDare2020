using Events;
using JetBrains.Annotations;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Movement))]
public class BlobMovementOrchestrator : MonoBehaviour
{
    private Movement movement;
    private BlobVision vision;
    private Sentient sentient;
    private IPrioritiesEvaluator priorityEvaluator;

    [SerializeField]
    private AMovementStrategy _availableStarts;

    [ShowNonSerializedField]
    private AMovementStrategy selectedMovementStrat;

    private bool canAssignPriorities = false;

    [UsedImplicitly]
    private void Awake()
    {
        movement = GetComponent<Movement>();
        vision = GetComponentInChildren<BlobVision>();
        sentient = GetComponent<Sentient>();
        Assert.IsNotNull(_availableStarts);

        // change this later;
        selectedMovementStrat = _availableStarts;
        canAssignPriorities = typeof(IAwareMover).IsAssignableFrom(selectedMovementStrat.GetType());
        priorityEvaluator = sentient.GetComponent<IPrioritiesEvaluator>();
    }

    [UsedImplicitly]
    private void FixedUpdate()
    {
        if(sentient != null && sentient.InInteractionRange)
        {
            sentient.Interact();
            return;
        }
        if(movement.CanReceiveInput)
        {
            var seenInteractions = vision?.AllInVision;
            if(canAssignPriorities)
            {
                var priorities = priorityEvaluator?.GetTargetPriorities();
                if(priorities != null)
                {
                    (selectedMovementStrat as IAwareMover).SetPriorities(priorities);
                }
                else
                {
                    UnityEngine.Debug.LogError($"Assigned smart priorities Strategies move selector, but sentient object has no way to get priorities on {name}");
                }
            }
            if(selectedMovementStrat.GetNextMovement(seenInteractions, transform.position, out var move))
            {
                movement.RegisterAction(move);
            }
        }
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
        // Uh-o, we need to inform somebody, that  we are dead
        MessageBroker.Default.Publish<BlobDeadEvt>(new BlobDeadEvt() { Blob = gameObject });
    }
}
