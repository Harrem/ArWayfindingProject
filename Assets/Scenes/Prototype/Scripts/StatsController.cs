using UnityEngine;
using UnityEngine.UI;
using TMPro;
enum Directions
{
    Stright,
    Left,
    Right,
    Back
}

public class StatsController : MonoBehaviour
{
    public RectTransform marker;
    public TMP_Text directionText;
    public TMP_Text destinationText;
    public NavigationController navController;
    public Transform selfPosition;
    public Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        destinationText.text = "Welcome to the University of Charmo";

    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = navController.getNextPoint();
        Vector3 directionToTarget = targetPosition - selfPosition.position;

        // float angle = Vector3.Angle(directionToTarget, Vector3.forward);
        // get the angle between the direction to target and the forward vector

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        switch (angle)
        {
            case float n when (n >= 0 && n <= 45):
                directionText.text = "Stright";
                break;
            case float n when (n >= 45 && n <= 135):
                directionText.text = "Left";
                break;
            case float n when (n >= 135 && n <= 225):
                directionText.text = "Back";
                break;
            case float n when (n >= 225 && n <= 315):
                directionText.text = "Right";
                break;
            case float n when (n >= 315 && n <= 360):
                directionText.text = "Stright";
                break;
        }
        // adjust the angle to account for the rotation of the canvas
        angle -= marker.transform.eulerAngles.z;

        // apply the rotation to the marker using the RectTransform component
        marker.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
