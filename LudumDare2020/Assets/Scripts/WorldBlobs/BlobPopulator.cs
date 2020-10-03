using Events;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class BlobPopulator : MonoBehaviour
{
    [SerializeField]
    private GameObject _blobPrefab;
    [SerializeField]
    private int _targetBlobNo;
    [SerializeField]
    private float _spawnRatePerSecond;

    [SerializeField]
    private int SPAWNEDCOUND;

    private float _spawnFraction = 0f;
    private List<GameObject> blobTracker = new List<GameObject>();

    private void Awake()
    {
        var stream = MessageBroker.Default.Receive<BlobDeadEvt>();
        stream.Subscribe(x => blobTracker.Remove(x.Blob));
    }

    [UsedImplicitly]
    private void FixedUpdate()
    {
        if (blobTracker?.Count < _targetBlobNo)
        {

            var toSpawnFloat = _spawnFraction + _spawnRatePerSecond * Time.fixedDeltaTime;
            var toSpawn = Mathf.FloorToInt(toSpawnFloat);
            _spawnFraction = toSpawnFloat - toSpawn;

            while (toSpawn > 0)
            {
                var blob = Instantiate(_blobPrefab);
                blob.name = $"Blob No: {blobTracker.Count}";

                BlobPositioner.PleaseBeThere.PositionNewBlob(blob.transform);

                blobTracker.Add(blob);
                SPAWNEDCOUND = blobTracker.Count;
                toSpawn--;
            }
        }
    }
}
