using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultSettingsController : MonoBehaviour
{
    public List<GameDifficult> settingsList = new List<GameDifficult>();
    public TMP_Dropdown difficultLevelDropdown;

    [Header("Difficulty level")]
    public string difficultName;
    public float spawnInterval;
    public List<string> enemyTypes;
    public int initialArmyLimit;
    public int armyLimitIncrease;

    public void SetSettings(List<GameDifficult> settings)
    {
        settingsList = settings;
    }

    public void SetupDifficult()
    {
        int numOfDifficult = difficultLevelDropdown.value;

        difficultName = settingsList[numOfDifficult].difficultName;
        spawnInterval = settingsList[numOfDifficult].enemySpawnInterval;
        enemyTypes = settingsList[numOfDifficult].enemyTypes;
        initialArmyLimit = settingsList[numOfDifficult].initialArmyLimit;
        armyLimitIncrease = settingsList[numOfDifficult].armyLimitIncrease;
    }
}
