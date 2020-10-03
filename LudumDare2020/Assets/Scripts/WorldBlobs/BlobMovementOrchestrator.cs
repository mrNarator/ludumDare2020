using Events;
using JetBrains.Annotations;
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
    private Movement _movement;
    [SerializeField]
    private AMovementStrategy _selectedMovementStrat;

    [UsedImplicitly]
    private void Awake()
    {
        _movement = GetComponent<Movement>();
        Assert.IsNotNull(_selectedMovementStrat);
    }

    [UsedImplicitly]
    private void Update()
    {
        if(_movement.CanReceiveInput)
        {
            _movement.RegisterAction(_selectedMovementStrat.GetNextMovement());
        }
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
        // Uh-o, we need to inform somebody, that  we are dead
        MessageBroker.Default.Publish<BlobDeadEvt>(new BlobDeadEvt() { Blob = gameObject });
    }
}
