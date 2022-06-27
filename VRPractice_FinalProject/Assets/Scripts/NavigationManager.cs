using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    void Start()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
