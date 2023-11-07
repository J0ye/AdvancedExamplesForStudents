using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrientation : MonoBehaviour
{
    public KeyCode activationKey = KeyCode.Mouse0;
    [Range(1f, 10f)]
    public float sensitivity = 3f;
    [Range(1f, 10f)]
    public float smooth = 2f;

    private Vector2 mouseOrientation;
    private Vector2 touchOrientation;
    private Vector2 smoothedVector;
    private Vector2 lookVector;

    // Update is called once per frame
    void Update()
    {
        // Dont run the Update method unless the activation key is pressed
        if (Input.GetKey(activationKey))
        {
            // Calculation of camera orientation by mouse
            mouseOrientation = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            // Add sensitivity and smooth by multiplying 
            // Vector2.Scale = Vector a * Vector b
            mouseOrientation = Vector2.Scale(mouseOrientation, new Vector2(sensitivity * smooth, sensitivity * smooth));
            smoothedVector = new Vector2(Mathf.Lerp(smoothedVector.x, mouseOrientation.x, 1.0f / smooth),
                                            Mathf.Lerp(smoothedVector.y, mouseOrientation.y, 1.0f / smooth));
            lookVector += smoothedVector;
            lookVector.y = Mathf.Clamp(lookVector.y, -90, 90);
        }

        // Translation of camera orientation
        transform.rotation = Quaternion.Euler(0, lookVector.x, 0);
    }
}

