using UnityEngine;using UnityEngine.AI;
public class NavMeshBaker : MonoBehaviour{ 
    [SerializeField] NavMeshSurface[] navMeshSurfaces;
    void Awake()
    {
        BakeNavMesh();
    }
    void Update()
    {
        BakeNavMesh();
    }
    public void BakeNavMesh(){ 
        foreach (var navMeshSurface in navMeshSurfaces)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}