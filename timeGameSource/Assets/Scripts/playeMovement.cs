//for any GetButtonDown questions go to Edit/ProjectSettings/InputManager, it lays out the buttons for all the inputs in this file
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playeMovement : MonoBehaviour
{

    List<string> inputs = new List<string>();

    public CharacterController2D controller; 

    public float runSpeed = 1f;//controls acceleration

    public float decelSpeed = -2 * 10f; //negative runspeed, makes code easier below because you just plug in move

    float horizontalAcceleration = 0f;//keep initialized at 0, gets edited every frame with the input value detected

    float horizontalDeceleration = 0f;//same as above

    public float frictionDeceleration = .5f;

    public Rigidbody2D rb;

    bool jump = false; 

    bool crouch = false;
    
    public timeStop getFrozen;//reference to timeStop script, checks to see if player is frozen. Use this to get freeze resource when we impliment that

    public float maxSpeed = 1f;//max speed

    void fillInputs()
    {
        for (int i = 0; i<inputs.Count; i++)
        {
            inputs[i] = "";
        }
    }

    void Start()
    {
        inputs.Add("");
        inputs.Add("");
        inputs.Add("");
        inputs.Add("");
        inputs.Add("");
        inputs.Add("");
    }
 

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump")&& !controller.m_Grounded)
        {
            inputs.Add("AirJump");
            inputs.RemoveAt(0); //I know AirJump is being added, but for some reason the buffer isn't working
        }
        else
        {
            inputs.Add("");
            inputs.RemoveAt(0);
        }
        
        horizontalAcceleration = Input.GetAxisRaw("Horizontal") * runSpeed; //checks to see if right or left input then assigns horizontal acceleration with that sign
        horizontalDeceleration = Input.GetAxisRaw("Horizontal") * decelSpeed; //same with deceleration
        if (((Input.GetButtonDown("Jump") && controller.m_Grounded) || (controller.m_Grounded && inputs.Contains("AirJump"))) && (!getFrozen.frozen) ) //pretty sure this is the problem
        {
            if (inputs.Contains("AirJump"))
            {
                Debug.Log("buffered");
            }
            jump = true; //sets jump to true if player is trying to jump. jump is passed as a parameter in move

            fillInputs();
        }

        if (Input.GetButtonDown("Crouch") && (!getFrozen.frozen))
        {
            crouch = true; //same as jump comment
        }
        else if (Input.GetButtonUp("Crouch") && (!getFrozen.frozen))
        {
            crouch = false; //same
        }
    }

    void FixedUpdate()
    {
        if(!getFrozen.frozen) { //Only move if we aren't frozen
            controller.Move(horizontalAcceleration * Time.fixedDeltaTime, crouch, jump);//player is allowed to accelerate normally
            jump = false;
            if(horizontalAcceleration == 0 && controller.m_Grounded) { //If we're grounded and not moving, decelerate (friction)
                if(rb.velocity.x < 0) { //If going to the left
                    if(Mathf.Abs(rb.velocity.x) < frictionDeceleration) //If we are moving slower than the friction deceleration, decrease by an amount equal to friction deceleration (ensures we don't go negative)
                        rb.velocity += new Vector2(rb.velocity.x, 0);
                    else {
                        rb.velocity += new Vector2(frictionDeceleration, 0); //If not moving slower than friction deceleration, decrease by friction deceleration
                    }
                }
                else if(rb.velocity.x > 0) { //If we're moving to the right
                    if(Mathf.Abs(rb.velocity.x) < frictionDeceleration) //If we are moving slower than the friction deceleration, decrease by an amount equal to friction deceleration (ensures we don't go negative)
                        rb.velocity -= new Vector2(rb.velocity.x, 0);
                    else {
                        rb.velocity -= new Vector2(frictionDeceleration, 0); //If not moving slower than friction deceleration, decrease by friction deceleration
                    }
                }
            }
        }
    }
}
