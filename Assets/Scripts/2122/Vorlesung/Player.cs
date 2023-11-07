using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce = 100f;
    public LayerMask groundLayer;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        if(!TryGetComponent<Rigidbody>(out rb))
        {
            Destroy(this);
        }

        try
        {
            rb = GetComponent<Rigidbody>();
        }
        catch(Exception e)
        {
            Debug.LogError(e);
            // rigidbody fehlt
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(GetInputAxis().x * speed, rb.velocity.y, GetInputAxis().z * speed);

        if(GetJumpInput())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    protected virtual Vector3 GetInputAxis()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate direction vectors based on orientation
        Vector3 forward = transform.forward * v;
        Vector3 sideways = transform.right * h;

        Vector3 re = forward + sideways;
        return re;
    }

    // Returns either 1 or -1 for keyboard. No values in between
    protected virtual Vector3 GetInputAxisRaw()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // calculate direction vectors based on orientation
        Vector3 forward = transform.forward * v;
        Vector3 sideways = transform.right * h;

        Vector3 re = forward + sideways;
        return re;
    }

    protected virtual bool GetJumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    protected virtual bool OnGround()
    {
        return Physics.OverlapSphere(transform.position, 0.1f, groundLayer).Length > 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
