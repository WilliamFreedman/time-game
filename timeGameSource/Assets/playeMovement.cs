
using UnityEngine;

public class playeMovement : MonoBehaviour
{

    public CharacterController2D controller; 

    public float runSpeed = 10f;

    float horizontalMove = 0f;

    bool jump = false;

    bool crouch = false;
    
    public timeStop getFrozen;
 

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
