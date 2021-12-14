using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrainController : MonoBehaviour
{
    public LayerMask renderOnOverview;

    private Animator anim;
    private LayerMask standard;
    private bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        standard = Camera.main.cullingMask;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            state = !state;

            if(state)
            {
                Camera.main.cullingMask = renderOnOverview;
            }else
            {
                Camera.main.cullingMask = standard;
            }
        }
        anim.SetBool("look", state);
    }
}
