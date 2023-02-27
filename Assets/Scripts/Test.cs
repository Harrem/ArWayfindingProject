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
        Debug.Log("Distance: "+CalculatePath(path.corners));
        
    }


    private float CalculatePath(Vector3[] path)
    {
        float distance = 0f;
        for(int i = 0; i< path.Length - 1; i++)
        {
            distance += Vector3.Distance(path[i], path[i+1]);
        }
        return distance;
    }
    
}