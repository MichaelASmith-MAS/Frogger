/* -----------------------------------------------------------------------------------
 * Class Name: Smith_Unparenting
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 08/07/2017
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;

public class Smith_Unparenting : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    GameObject player;

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
        player = GameObject.FindGameObjectWithTag(Smith_Tags.player);

	} //End Start
	
	// ------------------------------------------------------------------------------
	// Function Name: UnparentObject
	// Return types: N/A
	// Argument types: N/A
	// Author: 
	// Date: 
	// ------------------------------------------------------------------------------
	// Purpose: Runs each frame. Used to perform frame based checks and actions.
	// ------------------------------------------------------------------------------
	
	public void UnparentObject () 
	{

        if (player.transform.parent != null)
            player.transform.parent = null;

	} //End UnparentObject

    // ------------------------------------------------------------------------------
    // Function Name: UnparentObject
    // Return types: N/A
    // Argument types: N/A
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Runs each frame. Used to perform frame based checks and actions.
    // ------------------------------------------------------------------------------

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == Smith_Tags.player)
        {
            UnparentObject();
        }

    }

} // End Smith_Unparenting