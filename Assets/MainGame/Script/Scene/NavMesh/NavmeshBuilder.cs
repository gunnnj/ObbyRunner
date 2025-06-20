using UnityEngine;
using Unity.AI.Navigation;

public class NavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface surface;

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        BuildNavMesh();
    }

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }

    public void UpdateNavMesh()
    {
        surface.RemoveData();
        BuildNavMesh();
    }
}
