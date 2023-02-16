using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    private NavMeshPath path;
    private LineRenderer line;
    public Transform target;

    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.useWorldSpace = true;
    }

    private void Update()
    {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;

            line.SetPositions(path.corners);
    }
}