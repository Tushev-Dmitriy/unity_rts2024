using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AI_MapController : MonoBehaviour
{
    public NavMeshSurface mainAIofMap;

    public void NavMeshBuild(GameObject userBase)
    {
        mainAIofMap.BuildNavMesh();
        SetupNavMeshAgent(userBase);
    }

    public void SetupNavMeshAgent(GameObject userBase)
    {
        int numOfChild = userBase.transform.GetChild(0).childCount;
        for (int i = 3; i < numOfChild; i++)
        {
            NavMeshAgent agent = userBase.transform.GetChild(0).GetChild(i).gameObject.AddComponent<NavMeshAgent>();
            agent.baseOffset = 0f;
            agent.radius = 0.25f;
            agent.height = 5.2f;
        }

        mainAIofMap.BuildNavMesh();
    }
}
