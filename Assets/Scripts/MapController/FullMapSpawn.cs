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

    public void StartSpawn()
    {
        mapGenerator.SetupMapScale();
        generateBuilds.GenerateBuildsOnMap();
        difficultSettingsController.SetupDifficult();
        unitSpawn.UnitSpawner();
    }
}
