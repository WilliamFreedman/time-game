
using UnityEngine;

public class playeMovement : MonoBehaviour
{

    public CharacterController2D controller; 

    public float runSpeed = .5f;//controls acceleration

    public float decelSpeed = -1 * 10f; //negative runspeed, makes code easier below because you just plug in move

    float horizontalAcceleration = 0f;

    float horizontalDeceleration = 0f;

    bool jump = false;

    bool crouch = false;
    
    public timeStop getFrozen;

    public float maxSpeed = 1f;
 

    // Update is called once per frame
    void Update()
    {
        horizontalAcceleration = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalDeceleration = Input.GetAxisRaw("Horizontal") * decelSpeed;
        if (Input.GetButtonDown("Jump") && (!getFrozen.frozen))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch") && (!getFrozen.frozen))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch") && (!getFrozen.frozen))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {

        

        if (controller.m_Rigidbody2D.velocity.y == 0)//if stationary,move
        {
            controller.Move(horizontalAcceleration * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        else if (Mathf.Sign(horizontalAcceleration) == Mathf.Sign(controller.m_Rigidbody2D.velocity.x))//if moving in direction of input
        {

            if (controller.m_Rigidbody2D.velocity.x < maxSpeed)//if player is moving below max speed
            {
                controller.Move(horizontalAcceleration * Time.fixedDeltaTime, crouch, jump);
                jump = false;
            }
            
        }
        else if (Mathf.Sign(horizontalAcceleration) != Mathf.Sign(controller.m_Rigidbody2D.velocity.x)) // if not moving in direction of input
        {
            controller.Move(horizontalDeceleration * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

    }
}
