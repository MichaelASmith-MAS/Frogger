/* -----------------------------------------------------------------------------------
 * Class Name: Smith_Character_Movement
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 07/31/2017
 * -----------------------------------------------------------------------------------
 * Purpose: Controls character movement based on WASD key presses.
 * -----------------------------------------------------------------------------------
 */
 

using UnityEngine;

[RequireComponent (typeof (Rigidbody))]

public class Smith_Character_Movement : MonoBehaviour 
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float moveSpeed = 10f, maxDeltaV = 10f, turnSpeed = 2f, rayDistance = .2f, verticalOffset = .5f, jumpSpeed = 3f;
    public int hitAmount = 50, scorePickupChange = 50, timePickupChange = 10;
    public Transform lookPoint;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    Rigidbody rb;
	bool isFrozen;
    Smith_Points points;
    Smith_FrogSpawn spawn;
    Animation animator;
    //Adding sound for when frog..behaviors? -JS
    [SerializeField]
    private AudioClip frogWalkSFX, frogDeathSFX, frogJumpSFX;
    private AudioSource aSource;
    //end variables for frog behaviours.

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    public bool IsFrozen
    {
        get { return isFrozen; }
    }

    public Animation Animator
    {
        get { return animator; }
    }

    public Rigidbody RB
    {
        get { return rb; }
    }

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Awake
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/01/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs on startup before any other code. Runs prior to start.
    // ------------------------------------------------------------------------------

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        points = GetComponent<Smith_Points>();
        spawn = FindObjectOfType<Smith_FrogSpawn>();
        animator = GetComponent<Animation>();
        animator.Play(Smith_HashIDs.idle, PlayMode.StopAll);
        //Making reference to frog Audio Source - JS
        aSource = this.gameObject.GetComponent<AudioSource>();

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.freezeRotation = true;

        isFrozen = false;

    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 07/31/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs each frame. Used to perform frame based checks and actions.
    // ------------------------------------------------------------------------------

    void Update () 
	{
        if (!isFrozen)
        {
            Move();
            Jump();

        }

    } //End Update

    // ------------------------------------------------------------------------------
    // Function Name: Move
    // Return types: N/A
    // Argument types: 
    // Author: Michael Smith
    // Date: 08/09/2017
    // ------------------------------------------------------------------------------
    // Purpose: Regulates the movement of the frog
    // ------------------------------------------------------------------------------

    void Move ()
    {

        float v = Input.GetAxis(Smith_Tags.vertical);
        float h = Input.GetAxis(Smith_Tags.horizontal);

        Vector3 targetDirection = transform.forward * v + transform.right * h;

        Vector3 targetVelocity = targetDirection * moveSpeed;
        Vector3 deltaVelocity = targetVelocity - rb.velocity;

        deltaVelocity.x = Mathf.Clamp(deltaVelocity.x, -maxDeltaV, maxDeltaV);
        deltaVelocity.z = Mathf.Clamp(deltaVelocity.z, -maxDeltaV, maxDeltaV);
        deltaVelocity.y = 0f;

        //Make sure no other sound is playing, if not play frogWalk -JS
        if (!aSource.isPlaying)
        {
            aSource.PlayOneShot(frogWalkSFX);
        }

        if (v != 0)
        {
            animator.CrossFade(Smith_HashIDs.run);
            if (h < 0)
            {
                animator.CrossFade(Smith_HashIDs.leftTurn);

            }
            else if (h > 0)
            {
                animator.CrossFade(Smith_HashIDs.rightTurn);
            }

        }

        else if (h != 0)
        {
            if (h < 0)
            {
                animator.CrossFade(Smith_HashIDs.leftTurn);

            }

            else
            {
                animator.CrossFade(Smith_HashIDs.rightTurn);

            }

        }

        else
        {
            animator.CrossFade(Smith_HashIDs.idle);
            //Stop any playing sounds..? - JS
            aSource.Stop();

        }

        rb.AddForce(deltaVelocity, ForceMode.VelocityChange);

        lookPoint.position = new Vector3(transform.position.x, transform.position.y + verticalOffset, transform.position.z);

        if (v != 0 || h != 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0, lookPoint.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, turnSpeed * Time.deltaTime);
            
        }
        
    }

    // ------------------------------------------------------------------------------
    // Function Name: Jump
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/09/2017
    // ------------------------------------------------------------------------------
    // Purpose: Allows the frog to jump if on the ground
    // ------------------------------------------------------------------------------

    void Jump ()
    {

        if (Physics.Raycast(transform.position, -transform.up, rayDistance))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Stop any previous SFX and play jumpSFX
                aSource.Stop();
                aSource.volume = 0.5f;
                aSource.PlayOneShot(frogJumpSFX);//end SFX - JS

                animator.Play(Smith_HashIDs.jump, PlayMode.StopAll);
                rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
            }
            
        }
        
    }

    // ------------------------------------------------------------------------------
    // Function Name: FreezeCharacter
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 08/03/2017
    // ------------------------------------------------------------------------------
    // Purpose: Sets the character to frozen or not.
    // ------------------------------------------------------------------------------

    public void FreezeCharacter ()
    {
        isFrozen = !isFrozen;
        
    }

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerEnter
    // Return types: N/A
    // Argument types: Collider
    // Author: Michael Smith
    // Date: 08/03/2017
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void OnTriggerEnter (Collider collision)
    {
//____Added an 'OR' conjunction to if conditional to include enemy tag.
        if (collision.gameObject.tag == Smith_Tags.obstacle || collision.gameObject.tag == Smith_Tags.enemy)
        {
            Debug.Log("Hit obstacle: " + collision.name);

            animator.Play(Smith_HashIDs.death, PlayMode.StopAll);
            //Have frogDeath play when dying. - JS
            aSource.volume = 1.0f;
            aSource.PlayOneShot(frogDeathSFX);

            points.ChangePoints(-hitAmount);
            spawn.SpawnFrog();

        }

        if (collision.gameObject.tag == Smith_Tags.platform)
        {
            transform.parent = collision.gameObject.transform;
        }

    }

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerEnter
    // Return types: N/A
    // Argument types: Collider
    // Author: Michael Smith
    // Date: 08/07/2017
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == Smith_Tags.platform)
        {
            transform.parent = collision.gameObject.transform;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Smith_Tags.platform)
        {
            transform.parent = null;
        }

    }

} // End Smith_Character_Movement