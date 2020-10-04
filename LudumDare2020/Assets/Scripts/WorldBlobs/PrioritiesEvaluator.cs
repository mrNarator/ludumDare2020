using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Sentient))]
public class PrioritiesEvaluator : MonoBehaviour, IPrioritiesEvaluator
{
    private Settings.Blob _blobSettings;
    private Sentient sentient;

    [UsedImplicitly]
    private void Start()
    {
        _blobSettings = SettingsProvider.Instance.Global.BlobSettings;
        sentient = GetComponent<Sentient>();
        priorities = new List<InteractionType>();
        workList = new List<(InteractionType type, float perc, bool isCrit)>();
    }

    List<InteractionType> priorities;
    List<(InteractionType type, float perc, bool isCrit)> workList;

    public List<InteractionType> GetTargetPriorities()
    {
        priorities.Clear();
        workList.Clear();

        var foodPerc = sentient.Food / sentient.maxFood;
        workList.Add((type: InteractionType.Food, perc: foodPerc, isCrit: foodPerc < _blobSettings.AISettings.FoodCriticalPerc));

        var waterPerc = sentient.Drink / sentient.maxDrink;
        workList.Add((type: InteractionType.Drink, perc: waterPerc, isCrit: waterPerc < _blobSettings.AISettings.WaterCriticalPerc));

        var handsPerc = sentient.Handsome / sentient.maxHandsome;
        workList.Add((type: InteractionType.Handsome, perc: handsPerc, handsPerc < _blobSettings.AISettings.HandsCriticalPerc));

        var strengthPerc = sentient.Strength / sentient.maxStrength;
        workList.Add((type: InteractionType.Strength, perc: strengthPerc, strengthPerc < _blobSettings.AISettings.StrengthCriticalPerc));

        foreach(var (type, perc, isCrit) in workList)
        {
            if(isCrit)
            {
                priorities.Add(type);
            }
        }
        var missing = workList.Where(item => !item.isCrit).OrderBy(item => item.perc);
        foreach(var item in missing)
        {
            priorities.Add(item.type);
        }

        return priorities;
    }
}
