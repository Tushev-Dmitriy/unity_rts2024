using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMapSpawn : MonoBehaviour
{
    [Header("Another scripts")]
    public MapGenerator mapGenerator;
    public GenerateBuilds generateBuilds;
    public UnitSpawn unitSpawn;
    public DifficultSettingsController difficultSettingsController;
    public AI_MapController AI_MapController;

    public void StartSpawn()
    {
        mapGenerator.SetupMapScale();
        generateBuilds.GenerateBuildsOnMap();
        difficultSettingsController.SetupDifficult();
        unitSpawn.UnitSpawner();
        StartCoroutine(NavMeshMap());
    }

    IEnumerator NavMeshMap()
    {
        yield return new WaitForSeconds(0.05f);
        AI_MapController.mainAIofMap.BuildNavMesh();
        foreach (GameObject unit in unitSpawn.unitsInArray)
        {
            AI_MapController.SetupNavMeshAgent(unit);
        }
    }
}
