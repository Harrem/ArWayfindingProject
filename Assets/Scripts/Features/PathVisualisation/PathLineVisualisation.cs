using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PathLineVisualisation : MonoBehaviour {

    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Slider navigationYOffset;
    [SerializeField]
    private TextMeshProUGUI distanceTxt;

    private NavMeshPath path;
    private Vector3[] calculatedPathAndOffset;

    private void Update() {
        path = navigationController.CalculatedPath;
        AddOffsetToPath();
        AddLineOffset();
        SetLineRendererPositions();
    }

    private void AddOffsetToPath() {
        calculatedPathAndOffset = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++) {
            calculatedPathAndOffset[i] = new Vector3(path.corners[i].x, path.corners[i].y, path.corners[i].z);
            distanceTxt.text = CalculateDistance(path.corners).ToString("F1") + "M";
        }
    }

    private void AddLineOffset() {
        if (navigationYOffset.value != 0) {
            for (int i = 0; i < calculatedPathAndOffset.Length; i++) {
                calculatedPathAndOffset[i] += new Vector3(0, navigationYOffset.value, 0);
            }
        }
    }

    private void SetLineRendererPositions() {
        line.positionCount = calculatedPathAndOffset.Length;
        line.SetPositions(calculatedPathAndOffset);
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
