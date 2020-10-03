using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class SettingsProvider : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("_settings")]
    private Settings _global;

    public Settings Global => _global;

    private static SettingsProvider instance = null;
    // Game Instance Singleton
    public static SettingsProvider Instance
    {
        get {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
