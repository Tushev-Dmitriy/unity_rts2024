using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    
    private string filepath;
    private SettingsData data;


    private void Start()
    {
        filepath = Path.Combine(Application.streamingAssetsPath, "settings.json");
    }

    public void LoadSettings()
    {
        if (File.Exists(filepath))
        {
            data = JsonUtility.FromJson<SettingsData>(File.ReadAllText(filepath));
            Screen.SetResolution(data.width, data.height, data.window);
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
        File.WriteAllText(filepath, JsonUtility.ToJson(data));
        data.width = Screen.width;
        data.height = Screen.height;
        data.window = false;
        data.sounds = true;
        data.volume = volumeSlider.value;
    }
}
