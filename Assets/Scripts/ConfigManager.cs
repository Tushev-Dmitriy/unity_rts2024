using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public List<GameObject> settingsObj = new List<GameObject>();

    private string configFileName = "game_config.json";
    private GameConfig config;
    private List<int> resolutionsWidth = new List<int>() 
    {1920, 1600, 1366, 1280 };
    private List<int> resolutionsHeight = new List<int>() 
    {1080, 900, 768, 720 };

    private void Awake()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        string configFilePath = Path.Combine(Application.streamingAssetsPath, configFileName);

        if (File.Exists(configFilePath))
        {
            string json = File.ReadAllText(configFilePath);
            config = JsonUtility.FromJson<GameConfig>(json);
            SetupConfig(config);
        }
        else
        {
            SaveConfig();
        }
    }

    private void SetupConfig(GameConfig config)
    {
        SetResolution(config.numOfResolution, config.fullScreenMode);
        SetSoundAndMusicEnabled(config.soundAndMusicEnabled);
        SetVolume(config.volume);
    }

    public void SaveConfig()
    {
        if (config == null)
        {
            config = new GameConfig();
        }

        config.numOfResolution = settingsObj[0].GetComponent<TMP_Dropdown>().value;
        config.fullScreenMode = settingsObj[1].GetComponent<Toggle>().isOn;
        config.soundAndMusicEnabled = settingsObj[2].GetComponent<Toggle>().isOn;
        config.volume = settingsObj[3].GetComponent<Slider>().value;
        string configFilePath = Path.Combine(Application.streamingAssetsPath, configFileName);
        string json = JsonUtility.ToJson(config);
        File.WriteAllText(configFilePath, json);
        SetupConfig(config);
    }

    private void SetResolution(int numOfRes, bool fullScreen)
    {
        settingsObj[1].GetComponent<Toggle>().isOn = fullScreen;
        Screen.SetResolution(resolutionsWidth[numOfRes], resolutionsHeight[numOfRes], fullScreen);
    }

    private void SetSoundAndMusicEnabled(bool enabled)
    {
        settingsObj[2].GetComponent<Toggle>().isOn = enabled;
    }

    private void SetVolume(float volume)
    {
        settingsObj[3].GetComponent<Slider>().value = volume;
    }
}
