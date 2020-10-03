using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OutOfBoundDestroyer : MonoBehaviour
{
    [UsedImplicitly]
    private void OnTriggerEnter(Collider other)
    {

        var blob = other.GetComponentInChildren<BlobMovementOrchestrator>();
        if(blob != null)
        {
            Destroy(other.gameObject);
        }
    }
}
