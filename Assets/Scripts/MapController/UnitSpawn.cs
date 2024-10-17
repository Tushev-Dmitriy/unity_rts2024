using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    [Header("Other scripts")]
    public DifficultSettingsController difficultSettingsController;
    public UnitDataUI unitDataUI;
    public AI_MapController AI_MapController;

    [Header("Prefabs")]
    public GameObject[] units;

    [Header("Obj in inspector")]
    public GameObject userBase;
    public GameObject enemyBase;

    [Header("Difficult settings")]
    public float spawnInterval;
    public int numOfDifficult;
    public int armyLimitIncrease;
    public int initialArmyLimit;
    public List<int> numOfUnitsInArray;

    public List<GameObject> unitsInArray;

    [Header ("Pos to spawn")]
    public List<Vector3> unitSpawnPos = new List<Vector3>()
    {
        new Vector3(13, -8, 23), new Vector3(6, -8, 23), new Vector3(-1, -8, 23),
        new Vector3(-8, -8, 23), new Vector3(-15, -8, 23), new Vector3(13, -8, 28), 
        new Vector3(6, -8, 28), new Vector3(-1, -8, 28), new Vector3(-8, -8, 28), 
        new Vector3(-15, -8, 28)
    };
    public List<Vector3> catapultSpawnPos = new List<Vector3>()
    {
        new Vector3(22, -8, 50), new Vector3(0, -8, 50), new Vector3(-22, -8, 50)
    };

    public void UnitSpawner()
    {
        UserBaseUnitSpawn();
        EnemyBaseUnitSpawn();
    }

    private void UserBaseUnitSpawn()
    {
        Destroy(userBase.transform.GetChild(0).GetComponent<EnemySpawnController>());

        for (int i = 0; i < 3; i++)
        {
            GameObject villagerUnit = Instantiate(units[0], userBase.transform.GetChild(0).transform);
            villagerUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            villagerUnit.transform.localPosition = unitSpawnPos[i];
            unitDataUI.AddInfoOnUnit(villagerUnit);
            unitsInArray.Add(villagerUnit);
        }

        for (int j = 0; j < 2; j++)
        {
            GameObject archerUnit = Instantiate(units[1], userBase.transform.GetChild(0).transform);
            archerUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            archerUnit.transform.localPosition = unitSpawnPos[j + 3];
            unitDataUI.AddInfoOnUnit(archerUnit);
            unitsInArray.Add(archerUnit);
        }
    }

    public void EnemyBaseUnitSpawn()
    {
        spawnInterval = difficultSettingsController.spawnInterval;
        numOfDifficult = difficultSettingsController.enemyTypes.Count;
        initialArmyLimit = difficultSettingsController.initialArmyLimit;
        armyLimitIncrease = difficultSettingsController.armyLimitIncrease;
        numOfUnitsInArray = new List<int>();

        switch (numOfDifficult)
        {
            case 1:
                numOfUnitsInArray.Add(2);
                break;
            case 2:
                numOfUnitsInArray.Add(1);
                numOfUnitsInArray.Add(2);
                break;

            case 3:
                numOfUnitsInArray.Add(1);
                numOfUnitsInArray.Add(2);
                numOfUnitsInArray.Add(3);
                break;
        }

        for (int i = 0; i < enemyBase.transform.childCount; i++)
        {
            enemyBase.transform.GetChild(i).GetComponent<EnemySpawnController>().unitSpawn = gameObject.GetComponent<UnitSpawn>();
            enemyBase.transform.GetChild(i).GetComponent<EnemySpawnController>().AImapController = AI_MapController;
           enemyBase.transform.GetChild(i).GetComponent<EnemySpawnController>().StartSpawn(spawnInterval);
        }
    }
}
