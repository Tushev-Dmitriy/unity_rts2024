using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDifficulties
{
    public string difficultName;
    public float enemySpawnInterval;
    public List<string> enemyTypes;
    public int initialArmyLimit;
    public int armyLimitIncrease;
}
