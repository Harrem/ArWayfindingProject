using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    [SerializeField]
    private GameObject arCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var arY = arCamera.transform.eulerAngles.y;
        //var thisRotation = transform.rotation;
        //var rotation = new Quaternion(thisRotation.x, thisRotation.y, arY, thisRotation.w);
        //transform.rotation = rotation;
        transform.rotation = Quaternion.AngleAxis(-arY, Vector3.forward);
        //Debug.LogWarning("y: "+arY+" _ "+ "z: "+transform.rotation.z);
    }
}
