using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private string configFileName = "game_config.json";

    private GameConfig config;

    private void Awake()
    {
        LoadConfig();
    }

    public void LoadConfig()
    {
        string configFilePath = Path.Combine(Application.streamingAssetsPath, configFileName);

        if (File.Exists(configFilePath))
        {
            string json = File.ReadAllText(configFilePath);
            config = JsonUtility.FromJson<GameConfig>(json);
        }
        else
        {
            config = new GameConfig();
            config.resolution = Screen.currentResolution;
            config.fullScreenMode = Screen.fullScreenMode;
            config.soundAndMusicEnabled = true;
            config.volume = 0.5f;
            SaveConfig();
        }
    }

    private void SaveConfig()
    {
        string configFilePath = Path.Combine(Application.streamingAssetsPath, configFileName);
        string json = JsonUtility.ToJson(config);
        Debug.Log(json);
        File.WriteAllText(configFilePath, json);
    }

    public void SetResolution(Resolution resolution)
    {
        config.resolution = resolution;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        SaveConfig();
    }

    public void SetFullScreenMode(FullScreenMode fullScreenMode)
    {
        config.fullScreenMode = fullScreenMode;
        Screen.fullScreenMode = fullScreenMode;
        SaveConfig();
    }

    public void SetSoundAndMusicEnabled(bool enabled)
    {
        config.soundAndMusicEnabled = enabled;
        SaveConfig();
    }

    public void SetVolume(float volume)
    {
        config.volume = volume;
        SaveConfig();
    }
}
