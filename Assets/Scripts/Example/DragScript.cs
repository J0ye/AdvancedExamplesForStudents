using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : FollowMouse
{

    // Update is called once per frame
    protected override void Update()
    {
        transform.position = MousePos();

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(this);
        }
    }
}
