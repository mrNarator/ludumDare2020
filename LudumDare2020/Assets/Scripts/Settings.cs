using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "GlobalSettings", menuName = "GameSettings/Global")]
public class Settings : ScriptableObject
{
    public float DrinkDecaySpeed = 0.01f;
    public float MovementFoodCost = 0.1f;

    public Blob BlobSettings;

    [Serializable]
    public class Blob
    {
        [Range(0, 25)]
        public float VisioRange;

        public float MaxStatMagicNumber;

        public bool ShowDebug = true;

        public AI AISettings;

        [Serializable]
        public class AI
        {
            public float FoodCriticalPerc;
            public float WaterCriticalPerc;
            public float HandsCriticalPerc;
            public float StrengthCriticalPerc;
        }
    }
}
