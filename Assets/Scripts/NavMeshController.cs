using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface surface;

    public void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }
}
