using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public UnitSpawn unitSpawn;
    public AI_MapController AImapController;

    public void StartSpawn(float delay)
    {
        StartCoroutine(StartSpawnEnemyUnits(delay));
    }

    public IEnumerator StartSpawnEnemyUnits(float delay)
    {
        GameObject tempEnemyBase = gameObject;
        int numOfCatapult = 0;

        for (int j = 0; j < unitSpawn.initialArmyLimit; j++)
        {
            yield return new WaitForSeconds(delay);

            int tempEnemyNum = Random.Range(0, unitSpawn.numOfUnitsInArray.Count);

            if (tempEnemyNum == 2 && numOfCatapult < 3)
            {
                numOfCatapult++;
            }
            else
            {
                while (tempEnemyNum == 2)
                {
                    tempEnemyNum = Random.Range(0, unitSpawn.numOfUnitsInArray.Count);
                }
            }

            GameObject tempEnemyUnit = Instantiate(unitSpawn.units[unitSpawn.numOfUnitsInArray[tempEnemyNum]], tempEnemyBase.transform);
            tempEnemyUnit.tag = "EnemyUnit";
            unitSpawn.unitDataUI.AddInfoOnUnit(tempEnemyUnit);

            if (tempEnemyNum == 2)
            {
                tempEnemyUnit.transform.localPosition = unitSpawn.catapultSpawnPos[numOfCatapult - 1];
            }
            else
            {
                tempEnemyUnit.transform.localPosition = unitSpawn.unitSpawnPos[j];
            }
            tempEnemyUnit.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            AImapController.SetupNavMeshAgent(tempEnemyUnit);

            tempEnemyUnit.AddComponent<EnemyUnitResource>();
            tempEnemyUnit.GetComponent<EnemyUnitResource>().SetEnemyUnitInfo();
        }
    }
}
