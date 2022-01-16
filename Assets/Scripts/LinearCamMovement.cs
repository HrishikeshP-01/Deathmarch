using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCamMovement : MonoBehaviour
{
    public float speed = 0.06f, zoomSpeed = 10.0f, rotateSpeed=0.01f;
    // max & min. height the camera can go
    public float minHeight = 4f, maxHeight =40f;

    // Store mouse position
    Vector2 p1;
    Vector2 p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hsp = speed * Input.GetAxis("Horizontal"); // Horizontal speed
        float vsp = speed * Input.GetAxis("Vertical"); // Vertical speed
        float scrollsp = -zoomSpeed * Input.GetAxis("Mouse ScrollWheel"); // Scroll speed

        // Clamp Scrolling if camera is at min or max height
        if ((transform.position.y >= maxHeight) && scrollsp > 0)
        {
            scrollsp = 0;
        }
        else if ((transform.position.y <= minHeight) && scrollsp < 0)
        {
            scrollsp = 0;
        }
        /*But even this doesn't clamp it and the camera can still clip through
         this is because if the scroll speed is high enough then even if it passes the
        above condition when it gets added to move the position breaks the clamping barrier.
        
         Just because you check the present value doesn't mean the future value wont break the limit.
        So take the necessary precautions for that.
        */
        if ((transform.position.y + scrollsp) > maxHeight)
        {
            scrollsp = maxHeight - transform.position.y;
        }
        else if ((transform.position.y + scrollsp) < minHeight)
        {
            scrollsp = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0f, scrollsp, 0); // Zoom
        Vector3 lateralMove = hsp * transform.right; // left-right movement relative to camera (local space)
        /*Sometimes the camera might be pointing to the ground.
         If we use the forward vector we would hit the gound.
        To move forward even while pointing downwards, we use Vector Projection Method
        */
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0; // set y to 0 we only need x
        forwardMove.Normalize(); // get unit direction vector
        forwardMove *= vsp; // to ensure it mantains the same speed

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        getCameraRotation();
    }

    void getCameraRotation()
    {
        if (Input.GetMouseButtonDown(2)) // Check if middle mouse button is pressed
        {
            p1 = Input.mousePosition; //Get start pt of the mouse
        }
        if (Input.GetMouseButton(2)) // Check if middle mouse button is held down for the current frame
        { 
            p2 = Input.mousePosition; //Get end pt of the mouse

            // Find how much the mouse moved in between the last 2 frames
            float dx = (p1 - p2).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            // Rotate left and right 
            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));

            // Rotate up & down
            // Camera is the 1st & only child of the cam controller
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

        }
    }
}
