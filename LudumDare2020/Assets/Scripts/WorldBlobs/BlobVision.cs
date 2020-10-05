using Events;
using Generic;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class BlobVision : MonoBehaviour
{
    private Settings.Blob settings;

    private List<IInteractable> allTracked = new List<IInteractable>();

    public List<IInteractable> AllInVision => allTracked;

    private IDisposable eventStream;

    [UsedImplicitly]
    private void Awake()
    {
        settings = SettingsProvider.Instance.Global.BlobSettings;
        var collider = GetComponent<SphereCollider>();
        collider.radius = settings.VisioRange;

        eventStream = MessageBroker.Default.Receive<InteractableConsumedEvt>().Subscribe(OnInteractibleConsumed);
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
        eventStream.Dispose();
        eventStream = null;
    }

    [UsedImplicitly]
    private void OnTriggerEnter(Collider other)
    {

        var interactOptions = GetInteractables(other.gameObject);
        allTracked.AddRange(interactOptions);
    }

    [UsedImplicitly]
    private void OnTriggerExit(Collider other)
    {
        var interactOptions = GetInteractables(other.gameObject);
        allTracked.RemoveRange(interactOptions);
    }

    private void OnInteractibleConsumed(InteractableConsumedEvt evt)
    {
        var interactOptions = GetInteractables(evt.Obj);

        var count = allTracked.Count;
        allTracked.RemoveRange(interactOptions);
    }

    private IInteractable[] GetInteractables(GameObject go)
    {
        var intereactables = go.GetComponentsInParent<IInteractable>(true);
        return intereactables;
    }

    private List<IInteractable> GetOfType(InteractionType type)
    {
        return allTracked.Where(x => x.GetInteractionType() == type).ToList();
    }

    [UsedImplicitly]
    private void OnDrawGizmos()
    {
        if (settings == null || !settings.ShowDebug) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, settings.VisioRange);
        Gizmos.color = Color.white;
    }
}
