//for any GetButtonDown questions go to Edit/ProjectSettings/InputManager, it lays out the buttons for all the inputs in this file
using UnityEngine;

public class playeMovement : MonoBehaviour
{

    public CharacterController2D controller; 

    public float runSpeed = .5f;//controls acceleration

    public float decelSpeed = -1 * 10f; //negative runspeed, makes code easier below because you just plug in move

    float horizontalAcceleration = 0f;//keep initialized at 0, gets edited every frame with the input value detected

    float horizontalDeceleration = 0f;//same as above

    bool jump = false; 

    bool crouch = false;
    
    public timeStop getFrozen;//reference to timeStop script, checks to see if player is frozen. Use this to get freeze resource when we impliment that

    public float maxSpeed = 1f;//max speed
 

    // Update is called once per frame
    void Update()
    {
        horizontalAcceleration = Input.GetAxisRaw("Horizontal") * runSpeed; //checks to see if right or left input then assigns horizontal acceleration with that sign
        horizontalDeceleration = Input.GetAxisRaw("Horizontal") * decelSpeed; //same with deceleration
        if (Input.GetButtonDown("Jump") && (!getFrozen.frozen)) //pulls from getFrozen to see if player is frozen
        {
            jump = true; //sets jump to true if player is trying to jump. jump is passed as a parameter in move
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

        if (controller.m_Rigidbody2D.velocity.y == 0)//if stationary, player is allowed to move
        {
            controller.Move(horizontalAcceleration * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        else if (Mathf.Sign(horizontalAcceleration) == Mathf.Sign(controller.m_Rigidbody2D.velocity.x))//if player is moving in direction of input
        {

            if (controller.m_Rigidbody2D.velocity.x < maxSpeed)//if player is moving below max speed
            {
                controller.Move(horizontalAcceleration * Time.fixedDeltaTime, crouch, jump);//player is allowed to accelerate normally
                jump = false;
            }
            //if player is moving in direction of input and is at or above max speed, nothing should happen
            
        }
        else if (Mathf.Sign(horizontalAcceleration) != Mathf.Sign(controller.m_Rigidbody2D.velocity.x)) // if not moving in direction of input
        {
            controller.Move(horizontalDeceleration * Time.fixedDeltaTime, crouch, jump); //decelerates according to horizontalDeceleration
            jump = false;
        }

    }
}
