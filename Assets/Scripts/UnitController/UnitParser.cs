using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class UnitParser : MonoBehaviour
{
    public UnitDataUI unitDataUI;

    private const string unitsFileName = "units.json";

    private void Start()
    {
#if UNITY_EDITOR
        LoadUnits();
#elif UNITY_STANDALONE_WIN
        Debug.Log(1);
        List<ScriptableObject> createdUnits = new List<ScriptableObject>();
        ScriptableObject archerUnit = Resources.Load<ScriptableObject>("Units/Archer");
        ScriptableObject builderUnit = Resources.Load<ScriptableObject>("Units/Builder");
        ScriptableObject catapultUnit = Resources.Load<ScriptableObject>("Units/Catapult");
        ScriptableObject healerUnit = Resources.Load<ScriptableObject>("Units/Healer");
        ScriptableObject heavyWarriorUnit = Resources.Load<ScriptableObject>("Units/HeavyWarrior");
        ScriptableObject siegeTowerUnit = Resources.Load<ScriptableObject>("Units/SiegeTower");
        ScriptableObject spearmanUnit = Resources.Load<ScriptableObject>("Units/Spearman");
        createdUnits.Add(archerUnit);
        createdUnits.Add(builderUnit);
        createdUnits.Add(catapultUnit);
        createdUnits.Add(healerUnit);
        createdUnits.Add(heavyWarriorUnit);
        createdUnits.Add(siegeTowerUnit);
        createdUnits.Add(spearmanUnit);
        unitDataUI.SetUnits(createdUnits);
#endif
    }

    private void LoadUnits()
    {
#if UNITY_EDITOR
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

            List<ScriptableObject> createdUnits = new List<ScriptableObject>();

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
                        attackUnit.maxHealth = unitData.maxHealth;
                        attackUnit.trainingCost = unitData.trainingCost;
                        attackUnit.detectionRadius = unitData.detectionRadius;
                        attackUnit.icon = unitData.icon;
                        attackUnit.minRange = unitData.minRange;
                        attackUnit.maxRange = unitData.maxRange;
                        attackUnit.attackDelay = unitData.attackDelay;
                        attackUnit.damage = unitData.damage;
                        createdUnits.Add(attackUnit);
                        scriptableUnit = attackUnit;
                        break;

                    case Type.Builder:
                        var builderUnit = ScriptableObject.CreateInstance<Builder>();
                        builderUnit.unitName = unitData.unitName;
                        builderUnit.movementSpeed = unitData.movementSpeed;
                        builderUnit.health = unitData.health;
                        builderUnit.maxHealth = unitData.maxHealth;
                        builderUnit.trainingCost = unitData.trainingCost;
                        builderUnit.items = unitData.items;
                        builderUnit.detectionRadius = unitData.detectionRadius;
                        builderUnit.icon = unitData.icon;
                        builderUnit.resourceGatheringSpeed = unitData.resourceGatheringSpeed;
                        builderUnit.repairSpeed = unitData.repairSpeed;
                        builderUnit.repairEfficiency = unitData.repairEfficiency;
                        createdUnits.Add(builderUnit);
                        scriptableUnit = builderUnit;
                        break;

                    case Type.Healer:
                        var healerUnit = ScriptableObject.CreateInstance<Healer>();
                        healerUnit.unitName = unitData.unitName;
                        healerUnit.movementSpeed = unitData.movementSpeed;
                        healerUnit.health = unitData.health;
                        healerUnit.maxHealth = unitData.maxHealth;
                        healerUnit.trainingCost = unitData.trainingCost;
                        healerUnit.detectionRadius = unitData.detectionRadius;
                        healerUnit.icon = unitData.icon;
                        healerUnit.minDist = unitData.minDist;
                        healerUnit.maxDist = unitData.maxDist;
                        healerUnit.healDelay = unitData.healDelay;
                        healerUnit.heal = unitData.heal;
                        createdUnits.Add(healerUnit);
                        scriptableUnit = healerUnit;
                        break;

                    case Type.SiegeTower:
                        var siegeTower = ScriptableObject.CreateInstance<SiegeTower>();
                        siegeTower.unitName = unitData.unitName;
                        siegeTower.movementSpeed = unitData.movementSpeed;
                        siegeTower.health = unitData.health;
                        siegeTower.maxHealth = unitData.maxHealth;
                        siegeTower.trainingCost = unitData.trainingCost;
                        siegeTower.detectionRadius = unitData.detectionRadius;
                        siegeTower.icon = unitData.icon;
                        siegeTower.capacity = unitData.capacity;
                        createdUnits.Add(siegeTower);
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
            unitDataUI.SetUnits(createdUnits);
        }
#endif
    }
}
