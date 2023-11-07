using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToTarget : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = target.position;
        }
        catch (Exception e)
        {
            Debug.LogError("There seems to be no target for " + gameObject.name + ". This lead to " + e);
        }
    }
}
