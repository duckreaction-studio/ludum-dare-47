using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Used to make sure the GO is on the NavMesh.
/// </summary>
public class NavMeshInstantiator : MonoBehaviour
{
    NavMeshAgent _agent;
    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        MoveToClosestNavMeshPoint();
    }

    [ContextMenu("Set position")]
    public void ContextMenuSetPosition()
    {
        Debug.Log("Is on navmesh : " + _agent.isOnNavMesh);
        MoveToClosestNavMeshPoint();
        Debug.Log("Is on navmesh after : " + _agent.isOnNavMesh);
    }

    public void MoveToClosestNavMeshPoint()
    {
        NavMeshHit closestHit;
        
        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");
    }
}
