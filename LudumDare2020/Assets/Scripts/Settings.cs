using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public float DrinkDecaySpeed = 0.01f;
    public float MovementFoodCost = 0.1f;

    private static Settings instance = null;

    // Game Instance Singleton
    public static Settings Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }
}
