//for any GetButtonDown questions go to Edit/ProjectSettings/InputManager, it lays out the buttons for all the inputs in this file
using UnityEngine;


public class playeMovement : MonoBehaviour
{
    public GameManager God;

    public CharacterController2D controller; 

    public float runSpeed = 1f;//controls acceleration

    public float decelSpeed = -2 * 22f; //negative runspeed, makes code easier below because you just plug in move

    float horizontalAcceleration;//keep initialized at 0, gets edited every frame with the input value detected

    float horizontalDeceleration;//same as above

    public float frictionDeceleration = .5f;

    public Rigidbody2D rb;

    bool jump = false; 

    bool crouch = false;
    
    public timeStop getFrozen;//reference to timeStop script, checks to see if player is frozen. Use this to get freeze resource when we impliment that

    public float maxSpeed = 1f;//max speed

    float? timeOfInput;

    float bufferLength = .15f;

    
    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.collider.tag == "pusher")
        {
            rb.velocity = new Vector3(0,0,0);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "kill")
        {
            God.kill();
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Jump"))
        {
            timeOfInput = Time.time;
        }

        horizontalAcceleration = Input.GetAxisRaw("Horizontal") * runSpeed; //checks to see if right or left input then assigns horizontal acceleration with that sign
        horizontalDeceleration = Input.GetAxisRaw("Horizontal") * decelSpeed; //same with deceleration
        if (controller.m_Grounded && (!getFrozen.frozen) && (Input.GetButtonDown("Jump") || Time.time - timeOfInput < bufferLength)) //pretty sure this is the problem
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
