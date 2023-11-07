using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse2D : MonoBehaviour
{
    // The offset determines the direction the object will face
    // relative to the position of the mouse cursor.
    public Vector3 offset;
    public float reactionDistance = 1f;
    public float speed = 5f;
    public float dist;

    protected bool paused = false;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Debug.LogWarning("Cursor caught by " + name + ". To release press ESC.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) paused = !paused; // Switch pause on press ESC

        if(!paused)
        {
            // Get the current mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to world coordinates
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePositionInWorld = new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0);
            dist = Vector3.Distance(mousePositionInWorld, transform.position);


            if (dist > reactionDistance)
            {
                // Set the position of the object to the mouse position, with the z-coordinate
                // adjusted based on the offset. This will make the object appear above or below
                // the cursor based on the offset.
                transform.position += transform.right * speed * Time.deltaTime;
                print(transform.right * speed * Time.deltaTime);
                // Calculate the angle between the object's position and the mouse position
                float angle = Mathf.Atan2(
                    mousePositionInWorld.y - transform.position.y,
                    mousePositionInWorld.x - transform.position.x
                ) * Mathf.Rad2Deg;

                // Rotate the object to face the mouse cursor
                transform.rotation = Quaternion.Euler(offset.x, offset.y, angle);
            }
        }
    }
}
