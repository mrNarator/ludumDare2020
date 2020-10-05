using JetBrains.Annotations;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class PriorityCaps : MonoBehaviour
{
    private PrioritiesEvaluator evaler;

    private Settings.Colors _colorSettings;
    private Settings.Hats _hatSettings;

    private List<MeshRenderer> caps;

    [UsedImplicitly]
    private void Awake()
    {
        evaler = GetComponentInParent<PrioritiesEvaluator>();
        evaler.PrioritiesUpdated += OnUpdateCaps;
        caps = new List<MeshRenderer>();
    }

    [UsedImplicitly]
    private void Start()
    {
        _colorSettings = SettingsProvider.Instance.Global.ColorSettings;
        _hatSettings = SettingsProvider.Instance.Global.HatSettings;
    }

    private void OnUpdateCaps()
    {
        var newPriorities = evaler.Priorities;
        UpdateCapsSize(newPriorities.Count);
        for (int i = 0; i < caps.Count; i++)
        {
            if(i < newPriorities.Count)
            {
                var block = new MaterialPropertyBlock();
                block.SetColor("_Color", _colorSettings.Interactions.First(x => x.Type == newPriorities[i]).Color);
                caps[i].SetPropertyBlock(block);
                caps[i].gameObject.SetActive(true);
            }
            else
            {
                caps[i].gameObject.SetActive(false);
            }
        }
    }

    [Button]
    private void RegenHat()
    {
        var newSize = caps.Count;
        caps.ForEach(x => Destroy(x.gameObject));
        caps.Clear();
        UpdateCapsSize(newSize);
    }

    private void UpdateCapsSize(int newSize)
    {
        var initScale = _hatSettings.CapPrefab.transform.localScale;
        for (int i = caps?.Count ?? 0; i < newSize; i++)
        {
            var scaleDownValue = (1 - (float)i / newSize);
            var objScale = initScale * scaleDownValue;
            var mr = Instantiate(_hatSettings.CapPrefab, transform);
            var pos = mr.transform.localPosition;
            pos.y += _hatSettings.OffsetPerStep * i - _hatSettings.DistAdjust * (1 - scaleDownValue) * (1 - scaleDownValue);
            mr.transform.localPosition = pos;
            mr.transform.localScale = objScale;
            caps.Add(mr);
        }
    }
}
