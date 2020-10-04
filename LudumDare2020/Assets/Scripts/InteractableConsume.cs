using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class InteractableConsume : MonoBehaviour
{
    private bool consumed;
    public bool Consumed => consumed;

    public void MarkConsumed()
    {
        consumed = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        MessageBroker.Default.Publish(new InteractableConsumedEvt { Obj = gameObject });
    }
}
