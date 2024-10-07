using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] units;

    [Header("Obj in inspector")]
    public GameObject userBase;
    public GameObject enemyBase;
    public TMP_Dropdown difficultLevelDropdown;

    [Header("Difficulty level")]
    private string difficultyName;
    private int spawnInterval;
    private List<string> enemyTypes;
    private int initialArmyLimit;
    private int armyLimitIncrease;

    private List<Vector3> unitSpawnPos = new List<Vector3>()
    {
        new Vector3(13, -8, 23), new Vector3(6, -8, 23), new Vector3(-1, -8, 23),
        new Vector3(-8, -8, 23), new Vector3(-15, -8, 23)
    };

    public void UnitSpawner()
    {
        UserBaseUnitSpawn();
        EnemyBaseUnitSpawn();
    }

    private void UserBaseUnitSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject villagerUnit = Instantiate(units[0], userBase.transform.GetChild(0).transform);
            villagerUnit.transform.localRotation = new Quaternion(0, 180, 0, 0);
            villagerUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            villagerUnit.transform.localPosition = unitSpawnPos[i];
        }

        for (int j = 0; j < 2; j++)
        {
            GameObject archerUnit = Instantiate(units[1], userBase.transform.GetChild(0).transform);
            archerUnit.transform.localRotation = new Quaternion(0, 180, 0, 0);
            archerUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            archerUnit.transform.localPosition = unitSpawnPos[j + 3];
        }
    }

    private void EnemyBaseUnitSpawn()
    {
        
    }
}
