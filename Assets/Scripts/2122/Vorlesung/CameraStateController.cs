using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    public LayerMask overview;
    private Animator anim;
    private LayerMask standard;
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
            Debug.Log(anim.GetBool("look"));
            anim.SetBool("look", !anim.GetBool("look"));

            if(anim.GetBool("look"))
            {
                Camera.main.cullingMask = overview;
            } else
            {
                Camera.main.cullingMask = standard;
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(anim.GetFloat("State") == 2f)
            {
                anim.SetFloat("State", 0f);
            }
            else
            {
                anim.SetFloat("State", 2);
            }
        }
    }
}
