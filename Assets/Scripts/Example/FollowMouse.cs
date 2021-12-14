using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = MousePos();
    }

    protected virtual Vector3 MousePos()
    {
        Vector3 altMousePos = Input.mousePosition + (Vector3.forward * 5); 
        return new Vector3(Camera.main.ScreenToWorldPoint(altMousePos).x,
                3, Camera.main.ScreenToWorldPoint(altMousePos).z);
    }
}
