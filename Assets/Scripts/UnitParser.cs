using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class UnitParser : MonoBehaviour
{
    private const string unitsFileName = "units.json";

    private void Start()
    {
        LoadUnits();
    }

    private void LoadUnits()
    {
        string unitsFilePath = Path.Combine(Application.streamingAssetsPath, unitsFileName);

        if (File.Exists(unitsFilePath))
        {
            string json = File.ReadAllText(unitsFilePath);
            List<Unit> units = JsonConvert.DeserializeObject<List<Unit>>(json);

            string assetsPath = "Assets/Resources/Units";
            if (!AssetDatabase.IsValidFolder(assetsPath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Units");
            }

            foreach (Unit unitData in units)
            {
                ScriptableObject scriptableUnit = null;

                switch (unitData.unitType)
                {
                    case Type.AttackUnit:
                        var attackUnit = ScriptableObject.CreateInstance<AttackUnit>();
                        attackUnit.unitName = unitData.unitName;
                        attackUnit.movementSpeed = unitData.movementSpeed;
                        attackUnit.health = unitData.health;
                        attackUnit.trainingCost = unitData.trainingCost;
                        attackUnit.detectionRadius = unitData.detectionRadius;
                        attackUnit.icon = unitData.icon;
                        attackUnit.minRange = unitData.minRange;
                        attackUnit.maxRange = unitData.maxRange;
                        attackUnit.attackDelay = unitData.attackDelay;
                        attackUnit.damage = unitData.damage;
                        scriptableUnit = attackUnit;
                        break;

                    case Type.Builder:
                        var builderUnit = ScriptableObject.CreateInstance<Builder>();
                        builderUnit.unitName = unitData.unitName;
                        builderUnit.movementSpeed = unitData.movementSpeed;
                        builderUnit.health = unitData.health;
                        builderUnit.trainingCost = unitData.trainingCost;
                        builderUnit.detectionRadius = unitData.detectionRadius;
                        builderUnit.icon = unitData.icon;
                        builderUnit.resourceGatheringSpeed = unitData.resourceGatheringSpeed;
                        builderUnit.repairSpeed = unitData.repairSpeed;
                        builderUnit.repairEfficiency = unitData.repairEfficiency;
                        scriptableUnit = builderUnit;
                        break;

                    case Type.Healer:
                        var healerUnit = ScriptableObject.CreateInstance<Healer>();
                        healerUnit.unitName = unitData.unitName;
                        healerUnit.movementSpeed = unitData.movementSpeed;
                        healerUnit.health = unitData.health;
                        healerUnit.trainingCost = unitData.trainingCost;
                        healerUnit.detectionRadius = unitData.detectionRadius;
                        healerUnit.icon = unitData.icon;
                        healerUnit.minDist = unitData.minDist;
                        healerUnit.maxDist = unitData.maxDist;
                        healerUnit.healDelay = unitData.healDelay;
                        healerUnit.heal = unitData.heal;
                        scriptableUnit = healerUnit;
                        break;

                    case Type.SiegeTower:
                        var siegeTower = ScriptableObject.CreateInstance<SiegeTower>();
                        siegeTower.unitName = unitData.unitName;
                        siegeTower.movementSpeed = unitData.movementSpeed;
                        siegeTower.health = unitData.health;
                        siegeTower.trainingCost = unitData.trainingCost;
                        siegeTower.detectionRadius = unitData.detectionRadius;
                        siegeTower.icon = unitData.icon;
                        siegeTower.capacity = unitData.capacity;
                        scriptableUnit = siegeTower;
                        break;
                }

                if (scriptableUnit != null)
                {
                    string assetPath = Path.Combine(assetsPath, unitData.unitName + ".asset");
                    AssetDatabase.CreateAsset(scriptableUnit, assetPath);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
