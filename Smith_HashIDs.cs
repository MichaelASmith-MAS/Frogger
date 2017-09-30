/* -----------------------------------------------------------------------------------
 * Class Name: Smith_HashIDs
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;

public class Smith_HashIDs : MonoBehaviour
{
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public const string run = "Run";
    public const string jump = "Jump";
    public const string damage = "Take Damage1";
    public const string leftTurn = "Turn Left";
    public const string rightTurn = "Turn Right";
    public const string idle = "Idle";
    public const string death = "Take Damage2";

    public int attackBool;
    public int flyState;
    public int attackState;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    void Awake()
    {
        attackBool = Animator.StringToHash("Attack");
        flyState = Animator.StringToHash("Base Layer.Armature|Fly");
        attackState = Animator.StringToHash("Base Layer.Armature|Attac");

    }

} // End Smith_HashIDs