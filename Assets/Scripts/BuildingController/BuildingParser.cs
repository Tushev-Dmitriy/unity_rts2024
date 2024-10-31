using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildingParser : MonoBehaviour
{
    public BuildingDataUI buildingDataUI;

    private const string buildingsFileName = "buildings.json";

    private void Start()
    {
#if UNITY_EDITOR
        LoadBuildings();
#elif UNITY_STANDALONE_WIN
        List<ScriptableObject> createdBuildings = new List<ScriptableObject>();
        ScriptableObject barracks = Resources.Load<ScriptableObject>("Buildings/Barracks");
        ScriptableObject fields = Resources.Load<ScriptableObject>("Buildings/Fields");
        ScriptableObject house = Resources.Load<ScriptableObject>("Buildings/House");
        ScriptableObject mill = Resources.Load<ScriptableObject>("Buildings/Mill");
        ScriptableObject smeltery = Resources.Load<ScriptableObject>("Buildings/Smeltery");
        ScriptableObject temple = Resources.Load<ScriptableObject>("Buildings/Temple");
        ScriptableObject townHall = Resources.Load<ScriptableObject>("Buildings/TownHall");
        ScriptableObject warehouse = Resources.Load<ScriptableObject>("Buildings/Warehouse");
        ScriptableObject watchtower = Resources.Load<ScriptableObject>("Buildings/Watchtower");
        ScriptableObject workshop = Resources.Load<ScriptableObject>("Buildings/Workshop");
        createdBuildings.Add(barracks);
        createdBuildings.Add(fields);
        createdBuildings.Add(house);
        createdBuildings.Add(mill);
        createdBuildings.Add(smeltery);
        createdBuildings.Add(temple);
        createdBuildings.Add(townHall);
        createdBuildings.Add(warehouse);
        createdBuildings.Add(watchtower);
        createdBuildings.Add(workshop);
        buildingDataUI.SetBuildings(createdBuildings);
#endif
    }

    private void LoadBuildings()
    {
#if UNITY_EDITOR
            string buildingsFilePath = Path.Combine(Application.streamingAssetsPath, buildingsFileName);

        if (File.Exists(buildingsFilePath))
        {
            string json = File.ReadAllText(buildingsFilePath);
            List<Building> buildings = JsonConvert.DeserializeObject<List<Building>>(json);

            string assetsPath = "Assets/Resources/Buildings";
            if (!AssetDatabase.IsValidFolder(assetsPath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Buildings");
            }

            List<ScriptableObject> createdBuildings = new List<ScriptableObject>();

            foreach (Building buildingData in buildings)
            {
                ScriptableObject scriptableBuilding = null;

                switch (buildingData.buildingType)
                {
                    case BuildingType.Barracks:
                        var barracksBuilding = ScriptableObject.CreateInstance<Barracks>();
                        barracksBuilding.strength = buildingData.strength;
                        barracksBuilding.constructionCost = buildingData.constructionCost;
                        barracksBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(barracksBuilding);
                        scriptableBuilding = barracksBuilding;
                        break;

                    case BuildingType.House:
                        var houseBuilding = ScriptableObject.CreateInstance<House>();
                        houseBuilding.strength = buildingData.strength;
                        houseBuilding.constructionCost = buildingData.constructionCost;
                        houseBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(houseBuilding);
                        scriptableBuilding = houseBuilding;
                        break;

                    case BuildingType.Workshop:
                        var workshopBuilding = ScriptableObject.CreateInstance<Workshop>();
                        workshopBuilding.strength = buildingData.strength;
                        workshopBuilding.constructionCost = buildingData.constructionCost;
                        workshopBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(workshopBuilding);
                        scriptableBuilding = workshopBuilding;
                        break;

                    case BuildingType.Temple:
                        var templeBuilding = ScriptableObject.CreateInstance<Temple>();
                        templeBuilding.strength = buildingData.strength;
                        templeBuilding.constructionCost = buildingData.constructionCost;
                        templeBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(templeBuilding);
                        scriptableBuilding = templeBuilding;
                        break;

                    case BuildingType.Fields:
                        var fieldsBuilding = ScriptableObject.CreateInstance<Fields>();
                        fieldsBuilding.strength = buildingData.strength;
                        fieldsBuilding.constructionCost = buildingData.constructionCost;
                        fieldsBuilding.items = buildingData.items;
                        fieldsBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(fieldsBuilding);
                        scriptableBuilding = fieldsBuilding;
                        break;

                    case BuildingType.Mill:
                        var millBuilding = ScriptableObject.CreateInstance<Mill>();
                        millBuilding.strength = buildingData.strength;
                        millBuilding.constructionCost = buildingData.constructionCost;
                        millBuilding.items = buildingData.items;
                        millBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(millBuilding);
                        scriptableBuilding = millBuilding;
                        break;

                    case BuildingType.Watchtower:
                        var watchTowerBuilding = ScriptableObject.CreateInstance<Watchtower>();
                        watchTowerBuilding.strength = buildingData.strength;
                        watchTowerBuilding.constructionCost = buildingData.constructionCost;
                        watchTowerBuilding.detectionRadius = buildingData.detectionRadius;
                        watchTowerBuilding.buildingZoneRadius = buildingData.buildingZoneRadius;
                        watchTowerBuilding.archerCapacity = buildingData.archerCapacity;
                        watchTowerBuilding.attackRange = buildingData.attackRange;
                        watchTowerBuilding.attackDelay = buildingData.attackDelay;
                        watchTowerBuilding.damagePerArcher = buildingData.damagePerArcher;
                        createdBuildings.Add(watchTowerBuilding);
                        scriptableBuilding = watchTowerBuilding;
                        break;

                    case BuildingType.Warehouse:
                        var warehouseBuilding = ScriptableObject.CreateInstance<Warehouse>();
                        warehouseBuilding.strength = buildingData.strength;
                        warehouseBuilding.constructionCost = buildingData.constructionCost;
                        warehouseBuilding.resourceCapacityIncrease = buildingData.resourceCapacityIncrease;
                        createdBuildings.Add(warehouseBuilding);
                        scriptableBuilding = warehouseBuilding;
                        break;

                    case BuildingType.TownHall:
                        var townHallBuilding = ScriptableObject.CreateInstance<TownHall>();
                        townHallBuilding.strength = buildingData.strength;
                        townHallBuilding.constructionCost = buildingData.constructionCost;
                        townHallBuilding.items = buildingData.items;
                        townHallBuilding.buildingZoneRadius = buildingData.buildingZoneRadius;
                        createdBuildings.Add(townHallBuilding);
                        scriptableBuilding = townHallBuilding;
                        break;

                    case BuildingType.Smeltery:
                        var smelteryBuilding = ScriptableObject.CreateInstance<Smeltery>();
                        smelteryBuilding.strength = buildingData.strength;
                        smelteryBuilding.constructionCost = buildingData.constructionCost;
                        smelteryBuilding.items = buildingData.items;
                        smelteryBuilding.productOfBuilding = buildingData.productOfBuilding;
                        createdBuildings.Add(smelteryBuilding);
                        scriptableBuilding = smelteryBuilding;
                        break;
                }

                if (scriptableBuilding != null)
                {
                    string assetPath = Path.Combine(assetsPath, buildingData.buildingType + ".asset");
                    AssetDatabase.CreateAsset(scriptableBuilding, assetPath);
                }
            }

            AssetDatabase.SaveAssets();
            buildingDataUI.SetBuildings(createdBuildings);
        }
#endif
    }
}
