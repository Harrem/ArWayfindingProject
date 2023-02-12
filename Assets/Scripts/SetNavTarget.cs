using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SetNavTarget : MonoBehaviour
{

    [SerializeField]
    private TMP_Dropdown TargetsDropdown;
    [SerializeField]
    private List<Target> NavTargetObjects = new();

    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPosition = Vector3.zero;
    public float distanceLeft=0;

    private bool lineToggle = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
        Debug.LogWarning( path.status.ToString());
    }

    private void Update()
    {
        if (lineToggle && targetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            
        }
        CalculateDistance();
        Debug.LogWarning("Distance: "+ distanceLeft);
    }

    public void SetCurrentNavigationTarget(int selectedValue)
    {
        targetPosition = Vector3.zero;
        string selectedText = TargetsDropdown.options[selectedValue].text;
        Target currentTarget = NavTargetObjects.Find(x => x.Name.Equals(selectedText));
        if (currentTarget != null)
        {
            targetPosition = currentTarget.positionObject.transform.position;
        }
    }

    public void Togglevisibility()
    {
        lineToggle = !lineToggle;
        line.enabled = lineToggle;
    }

    public void CalculateDistance()
    {
        distanceLeft = 0f;
        foreach (var corner in path.corners)
        {
            var dis = Vector3.Distance(transform.position, corner);
            distanceLeft += dis;
        }
        Debug.LogWarning("Distance: "+ distanceLeft);
    }
}
