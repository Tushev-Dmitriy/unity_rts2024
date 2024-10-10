using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DifficultySettingsParser : MonoBehaviour
{
    [Header("Another scripts")]
    public DifficultSettingsController difficultSettingsController;

    private const string settingsFileName = "GameDifficultySettings.json";

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        string settingsFilePath = Path.Combine(Application.streamingAssetsPath, settingsFileName);

        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            List<GameDifficulties> settings = JsonConvert.DeserializeObject<List<GameDifficulties>>(json);

            string assetsPath = "Assets/Resources/Difficult";
            if (!AssetDatabase.IsValidFolder(assetsPath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Difficult");
            }

            List<GameDifficult> createdSettings = new List<GameDifficult>();

            foreach (GameDifficulties gameSetting in settings)
            {
                ScriptableObject scriptableSettings = null;

                GameDifficult gameSettings = ScriptableObject.CreateInstance<GameDifficult>();
                gameSettings.difficultName = gameSetting.difficultName;
                gameSettings.enemySpawnInterval = gameSetting.enemySpawnInterval;
                gameSettings.enemyTypes = gameSetting.enemyTypes;
                gameSettings.initialArmyLimit = gameSetting.initialArmyLimit;
                gameSettings.armyLimitIncrease = gameSetting.armyLimitIncrease;
                scriptableSettings = gameSettings;

                if (scriptableSettings != null)
                {
                    createdSettings.Add(gameSettings);

                    string assetPath = Path.Combine(assetsPath, gameSetting.difficultName + ".asset");
                    AssetDatabase.CreateAsset(scriptableSettings, assetPath);
                }
            }

            AssetDatabase.SaveAssets();

            difficultSettingsController.SetSettings(createdSettings);
        }
    }
}
