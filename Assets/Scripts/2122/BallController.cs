using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : PlayerController
{
    public float maxVelocity = 10f;
    public float handBreakDrag = 10f;
    public Transform targetOrientation;

    protected float startDrag;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        startDrag = rb.drag;
    }

    // Update is called once per frame
    public override void Update()
    {
        Vector3 inputVec = new Vector3(GetInputAxis().x * speed, 0, GetInputAxis().z * speed);
        rb.AddForce(inputVec, ForceMode.Force);
        Vector3 clamped = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);

        Debug.Log(OnGround());
        if (Input.GetButton("Jump") && OnGround())
        {
            Jump();
        }

        if(Input.GetButton("Fire2"))
        {
            rb.drag = handBreakDrag;
        } else
        {
            rb.drag = startDrag;
        }
    }
    protected override bool OnGround()
    {
        return Physics.OverlapSphere(transform.position - (Vector3.up / 2), 
            0.1f, groundLayer).Length > 0;
    }

    protected override Vector3 GetInputAxis()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate direction vectors based on orientation
        Vector3 forward = targetOrientation.forward * v;
        Vector3 sideways = targetOrientation.right * h;

        Vector3 re = forward + sideways;
        return re;
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position - (Vector3.up / 2), 0.1f);
    }
}
