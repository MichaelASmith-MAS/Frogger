/* -----------------------------------------------------------------------------------
 * Class Name: Smith_Camera_Controller
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Smith_Camera_Controller : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public Transform lookPoint;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    float mouseX, mouseY, mouseSensitivity = 2f;
    bool invertY = false, frozenCamera = false;

    // ------------------------------------------------------------------------------
    // GETTERS/SETTERS
    // ------------------------------------------------------------------------------

    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    public bool InvertY
    {
        get { return invertY; }
        set { invertY = value; }
    }

    public bool FrozenCamera
    {
        get { return frozenCamera; }
    }

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start
    // Return types: N/A
    // Argument types: N/A
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Used to initialize variables or perform startup processes
    // ------------------------------------------------------------------------------
    void Start () 
	{
        

	} //End Start
	
	// ------------------------------------------------------------------------------
	// Function Name: Update
	// Return types: N/A
	// Argument types: N/A
	// Author: 
	// Date: 
	// ------------------------------------------------------------------------------
	// Purpose: Runs each frame. Used to perform frame based checks and actions.
	// ------------------------------------------------------------------------------
	
	void Update () 
	{
        if (!frozenCamera)
        {
            CameraLook();
        }

	} //End Update

    // ------------------------------------------------------------------------------
    // Function Name: CameraLook
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/09/2017
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void CameraLook()
    {
        mouseX += Input.GetAxis(Smith_Tags.mouseX) * mouseSensitivity;

        if (invertY)
        {
            mouseY += Input.GetAxis(Smith_Tags.mouseY) * mouseSensitivity;
        }

        else
        {
            mouseY -= Input.GetAxis(Smith_Tags.mouseY) * mouseSensitivity;
        }

        transform.LookAt(lookPoint);

        mouseY = Mathf.Clamp(mouseY, -60, 60);

        lookPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

    }

    // ------------------------------------------------------------------------------
    // Function Name: CameraLook
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/09/2017
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    public void FreezeCamera ()
    {
        frozenCamera = !frozenCamera;

    }

} // End Smith_Camera_Controller