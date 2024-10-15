using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class AI_MapController : MonoBehaviour
{
    public NavMeshSurface mainAIofMap;

    public void NavMeshBuild()
    {
        mainAIofMap.BuildNavMesh();
    }
}
