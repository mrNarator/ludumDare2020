using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public class BlobPositioner : MonoBehaviour
{
    private static BlobPositioner _instance;
    public static BlobPositioner PleaseBeThere
    {
        get {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BlobPositioner>();
            }
            if(_instance == null)
            {
                throw new NullReferenceException($"{nameof(BlobPositioner)} could not be found in scene, are you sure it exists");
            }
            return _instance;
        }
    }
    [SerializeField]
    private float _circleRadius;
    [SerializeField]
    private Transform _newObjectsRoot;

    [UsedImplicitly]
    private void Awake()
    {
        if(_instance != this && _instance != null)
        {
            Destroy(gameObject);
            return;
        }
        if(_instance == null)
        {
            _instance = this;
            return;
        }
        if(_instance == this)
        {
            throw new Exception($"Why am I added two  times?");
        }
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }

    public void PositionNewBlob(Transform newBlob)
    {
        lastSpawnPos = FindNewPosition();
        newBlob.SetParent(transform);
        newBlob.localPosition = lastSpawnPos;
    }

    private Vector3 FindNewPosition()
    {
        var spawnPos2D = UnityEngine.Random.insideUnitCircle * _circleRadius;
        return spawnPos2D.ToVec3NormUp();
    }


    [Header("Debug")]
    [SerializeField]
    private bool showRanges = true;
    private Vector3 lastSpawnPos;

    [UsedImplicitly]
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(!showRanges)
        {
            return;
        }

        Gizmos.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, _circleRadius);
        Gizmos.DrawSphere(transform.TransformPoint(lastSpawnPos), 1f);
        Gizmos.color = Color.white;
#endif // UNITY_EDITOR
    }
}
