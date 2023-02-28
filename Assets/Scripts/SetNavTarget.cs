using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SetNavTarget : MonoBehaviour
{

    [SerializeField]
    private TMP_Dropdown TargetsDropdown;
    [SerializeField]
    private TextMeshProUGUI distanceTxt;
    [SerializeField]
    private List<Target> NavTargetObjects = new();
    [SerializeField]
    private Slider heightLine;
    [SerializeField]
    private GameObject miniMap;
    [SerializeField]
    private GameObject debugWindow;


    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPosition = Vector3.zero;

    //private int currentFloor = 1;

    private bool lineToggle = false;
    private bool miniMapToggle = true;
    private bool toggleDebugWindow = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    private void Update()
    {
        if (lineToggle && targetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            Vector3[] calculatedPath = AddLineOffset();
            line.SetPositions(calculatedPath);
            distanceTxt.text =  CalculateDistance(path.corners).ToString("F1") + "M";
        }
    }

    private Vector3[] AddLineOffset()
    {
        if (heightLine.value == 0)
        {
            return path.corners;
        }
        Vector3[] calculatedLine = new Vector3[path.corners.Length];
        for (int i = 0; i < calculatedLine.Length; i++)
        {
            calculatedLine[i] = path.corners[i] + new Vector3(0, heightLine.value, 0); 
        }
        return calculatedLine;
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
    public void ToggleMiniMap()
    {
        miniMapToggle = !miniMapToggle;
        miniMap.SetActive(miniMapToggle);
    }
    public void ToggleDebug()
    {
        toggleDebugWindow = !toggleDebugWindow;
        debugWindow.SetActive(toggleDebugWindow);
    }

    private float CalculateDistance(Vector3[] path)
    {
        float distance = 0f;
        for (int i = 0; i < path.Length - 1; i++)
        {
            distance += Vector3.Distance(path[i], path[i + 1]);
        }
        return distance;
    }
}
