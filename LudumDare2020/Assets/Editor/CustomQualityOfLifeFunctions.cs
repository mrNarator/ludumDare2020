using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editor
{
    public class CustomQualityOfLifeFunctions : EditorWindow
    {

        [MenuItem("Custom/Commands/Clear Player Prefs #&p")]
        [UsedImplicitly]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        [MenuItem("Custom/Commands/SpritePacker shortcut %&x")]
        [UsedImplicitly]
        public static void OpenSpritePacker()
        {
            var packerType = typeof(EditorWindow).Assembly.GetType("UnityEditor.Sprites.PackerWindow");
            Debug.LogFormat("packerType: {0}", packerType);
            EditorWindow.GetWindow(packerType);
        }

        [MenuItem("Custom/Shortcuts/Save Project #&S")]
        [UsedImplicitly]
        private static void SaveProject()
        {
            AssetDatabase.SaveAssets();
        }

    }
}
