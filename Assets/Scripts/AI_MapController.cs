using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AI_MapController : MonoBehaviour
{
    public NavMeshSurface mainAIofMap;

    public void SetupNavMeshAgent(GameObject unitObj)
    {
        NavMeshAgent agent = unitObj.AddComponent<NavMeshAgent>();
        agent.baseOffset = 0f;
        agent.radius = 0.25f;
        agent.height = 5.2f;

        mainAIofMap.BuildNavMesh();
    }
}
