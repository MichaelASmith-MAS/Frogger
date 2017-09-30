/* -----------------------------------------------------------------------------------
 * Class Name: Smith_FrogSpawn
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 08/03/2017
 * -----------------------------------------------------------------------------------
 * Purpose: Spawns the frog
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class Smith_FrogSpawn : MonoBehaviour 
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
    Smith_Character_Movement movement;

	// ------------------------------------------------------------------------------
	// FUNCTIONS
	// ------------------------------------------------------------------------------

	// ------------------------------------------------------------------------------
	// Function Name: Awake
	// Return types: N/A
	// Argument types: N/A
	// Author: Michael Smith
	// Date: 08/03/2017
	// ------------------------------------------------------------------------------
	// Purpose: Used to initialize variables or perform startup processes
	// ------------------------------------------------------------------------------
	void Awake () 
	{
        player = GameObject.FindGameObjectWithTag(Smith_Tags.player);
        movement = player.GetComponent<Smith_Character_Movement>();

	} //End Awake

    // ------------------------------------------------------------------------------
    // Function Name: SpawnFrog
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/03/2017
    // ------------------------------------------------------------------------------
    // Purpose: Moves the frog back to the spawn point
    // ------------------------------------------------------------------------------

    public void SpawnFrog ()
    {
        if (player.transform.parent != null)
        {
            player.transform.parent = null;
        }

        movement.FreezeCharacter();
        movement.RB.velocity = Vector3.zero;

        player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        player.GetComponent<Smith_Character_Movement>().lookPoint.position = new Vector3(transform.position.x, transform.position.y + player.GetComponent<Smith_Character_Movement>().verticalOffset, transform.position.z);

        movement.Animator.Play(Smith_HashIDs.death);

        StartCoroutine("PlayPause");

        

    }

    // ------------------------------------------------------------------------------
    // Function Name: SpawnFrog
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/03/2017
    // ------------------------------------------------------------------------------
    // Purpose: Moves the frog back to the spawn point
    // ------------------------------------------------------------------------------

    IEnumerator PlayPause ()
    {
        bool run = true;

        while (run)
        {
            
            yield return new WaitForSeconds(2);

            movement.FreezeCharacter();

            run = false;
        }


    }

} // End Smith_FrogSpawn