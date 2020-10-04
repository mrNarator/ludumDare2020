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

    [SerializeField]
    private AMovementStrategy _availableStarts;

    [ShowNonSerializedField]
    private AMovementStrategy selectedMovementStrat;

    [UsedImplicitly]
    private void Awake()
    {
        movement = GetComponent<Movement>();
        vision = GetComponentInChildren<BlobVision>();
        Assert.IsNotNull(_availableStarts);

        // change this later;
        selectedMovementStrat = _availableStarts;
    }

    [UsedImplicitly]
    private void Update()
    {
        if(movement.CanReceiveInput)
        {
            var seenInteractions = vision?.AllInVision;
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
