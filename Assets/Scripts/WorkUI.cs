using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class SettingsData
{
    public int width;
    public int height;
    public bool window;
    public bool sounds;
    public float volume;
}

public class WorkUI : MonoBehaviour
{
    public AudioMixer am;
    public Slider volumeSlider;
    public GameObject resDropdown;
    public Toggle windowToggle;
    public Toggle soundToggle;
    public GameObject windowOff;
    public GameObject soundOff;
    public TMP_Text windowToggleText;
    public TMP_Text soundToggleText;
    public GameObject settings;
    public GameObject checkSettings;

    private string filepath;
    private SettingsData data;
    private int width;
    private int height;
    private bool isWindow = false;
    private bool isSound = false;
    private bool saveInfo = false;

    private void Start()
    {
        filepath = Path.Combine(Application.streamingAssetsPath, "settings.json");
    }

    private void Update()
    {
        CheckBool();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitFromSettings()
    {
        if (saveInfo)
        {
            settings.SetActive(false);
        } else
        {
            checkSettings.SetActive(true);
        }
    }

    public void LoadSettings()
    {
        saveInfo = false;
        if (File.Exists(filepath))
        {
            data = JsonUtility.FromJson<SettingsData>(File.ReadAllText(filepath));
            Screen.SetResolution(data.width, data.height, !data.window);
            am.SetFloat("MainSound", data.volume);
            volumeSlider.value = data.volume;
        }
        else
        {
            data = new SettingsData();
            SaveField();
        }
    }

    public void SaveField()
    {
        ResController();
        data.width = width;
        data.height = height;
        data.window = isWindow;
        data.sounds = isSound;
        data.volume = volumeSlider.value;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filepath, json);
        LoadSettings();
        saveInfo = true;
    }

    private void CheckBool()
    {
        if (windowToggle.isOn)
        {
            isWindow = true;
            windowOff.SetActive(false);
            windowToggleText.text = "ON";
        } else
        {
            isWindow = false;
            windowOff.SetActive(true);
            windowToggleText.text = "OFF";
        }

        if (soundToggle.isOn)
        {
            isSound = true;
            soundOff.SetActive(false);
            soundToggleText.text = "ON";
        }
        else
        {
            isSound = false;
            soundOff.SetActive(true);
            soundToggleText.text = "OFF";
        }
    }

    private void ResController()
    {
        int a = resDropdown.GetComponent<TMP_Dropdown>().value; 
        
        switch (a)
        {
            case 0:
                width = 1920;
                height = 1080;
                break;
            case 1:
                width = 1600;
                height = 900;
                break;
            case 2:
                width = 1366;
                height = 768;
                break;
            case 3:
                width = 1280;
                height = 720;
                break;
        }
    }
}
