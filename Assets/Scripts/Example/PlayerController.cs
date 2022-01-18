using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 100f;
    public LayerMask groundLayer;

    protected Rigidbody rb;
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        rb.velocity = new Vector3(GetInputAxis().x * speed, rb.velocity.y, GetInputAxis().z * speed);
        if(GetJumpInput() && OnGround())
        {
            Jump();
        }

        List<Collider> boxArray = new List<Collider>(Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f), transform.rotation, groundLayer));
        if (boxArray.Count > 0)
        {
            Collider thisSHouldBeYourPlatformScript;
            foreach(Collider hitTarget in boxArray)
            {
                if(hitTarget.gameObject.TryGetComponent<Collider>(out thisSHouldBeYourPlatformScript))
                {
                    // + thisSHouldBeYourPlatformScript.positiondifference
                    transform.position += thisSHouldBeYourPlatformScript.transform.position;
                }
            }
        }
    }

    protected void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
