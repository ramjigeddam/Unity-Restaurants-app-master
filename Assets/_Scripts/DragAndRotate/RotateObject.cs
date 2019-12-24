using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion desiredRotation = transform.rotation;

        DetectTouchMovement.Calculate();

        if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
        { // rotate
            Debug.Log("Rotate");
            Vector3 rotationDeg = Vector3.zero;
            rotationDeg.y = -DetectTouchMovement.turnAngleDelta;
            desiredRotation *= Quaternion.Euler(rotationDeg);

        }


        // not so sure those will work:
        transform.rotation = desiredRotation;
        //transform.position += Vector3.forward * pinchAmount;
    }
}
