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
    public GameObject prefab;
    public bool isMarker = false;
    GameObject targetMarker;
    // Start is called before the first frame update
    void Start()
    {
        destinationText.text = "Welcome to the University of Charmo";
        targetMarker = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = navController.getNextPoint();
        targetMarker.transform.position = selfPosition.transform.position;
        targetMarker.transform.LookAt(targetPosition);

        // Get the difference in rotation between the two transforms
        float ang1 = selfPosition.transform.eulerAngles.y;
        float ang2 = targetMarker.transform.eulerAngles.y;
        float angleDifference = ang1 - ang2;

        // Print the angle difference to the console
        Debug.Log("Rotation difference: " + angleDifference);

        // Print the angle to the console

        switch (angleDifference)
        {
            case float n when (n >= 0 && n <= 45):
                directionText.text = "Stright Foward";
                break;
            case float n when (n >= 45 && n <= 135):
                directionText.text = " Turn Left";
                break;
            case float n when (n >= 135 && n <= 225):
                directionText.text = "Turn Back";
                break;
            case float n when (n >= 225 && n <= 315):
                directionText.text = "Turn Right";
                break;
            case float n when (n >= 315 && n <= 360):
                directionText.text = "Stright Foward";
                break;
        }

        // apply the rotation to the marker using the RectTransform component
        marker.rotation = Quaternion.Euler(new Vector3(0, 0, angleDifference));
    }

    public void AddMarker()
    {
        GameObject marker = Instantiate(prefab, targetPosition, Quaternion.identity);
    }
}
