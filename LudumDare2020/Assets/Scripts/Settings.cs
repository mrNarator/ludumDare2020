using NaughtyAttributes;
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
    public Colors ColorSettings;
    public Hats HatSettings;

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
            public float LoveCriticalPerc;
        }
    }

    [Serializable]
    public class Colors
    {
        [ReorderableList]
        public List<Interaction> Interactions;

        [ReorderableList]
        public List<MovementStrat> Movements;

        [Serializable]
        public class Interaction
        {
            public InteractionType Type;
            public Color Color;
        }

        [Serializable]
        public class MovementStrat
        {
            public AMovementStrategy Strat;
            public Color Color;
        }
    }

    [Serializable]
    public class Hats
    {
        public MeshRenderer CapPrefab;
        public float DistAdjust;
        public float OffsetPerStep;
    }
}
