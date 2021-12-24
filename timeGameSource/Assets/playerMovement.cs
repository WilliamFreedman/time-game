using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D player;
    // Jumping variables
    public Vector2 jumpForce; //force of a standing jump
    bool onGround; //stores if the player is on the ground to enable jumping

    //public stopTime timeVars;
    

    //running variables
    public static float runForce = 100;
    Vector2 rightRun = new Vector2(runForce,0);
    Vector2 leftRun = new Vector2(-1*runForce,0);
    public float maxSpeed = 15;

    //air movement variables
    public static float glideForce = 40;
    Vector2 rightGlide = new Vector2(glideForce,0);
    Vector2 leftGlide = new Vector2(-1*glideForce,0);
    



    void Start()
    {
        player.freezeRotation = true;
    }

    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag.Contains("ju"))
        {
            onGround = true; //when player hits ground, they gain the ability to jump 
        }
        if (other.collider.tag.Contains("jp"))
        {
            player.AddForce(new Vector2(600,700));
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag.Contains("ki"))
        {
            Debug.Log("ded");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKey("w") && onGround == true) // player has tp be pressing jump and on the ground
        {
            onGround = false; 
            player.AddForce(jumpForce); //enables jump then jumps, does order matter?
        }

        if (Input.GetKey("d") && !player.IsSleeping())
        {
            if (onGround)
            {
                if (player.velocity.magnitude < maxSpeed)
                {
                    player.AddForce(rightRun);
                }
            }
            else
            {
                {
                    player.AddForce(rightGlide);
                }
            }
            
            
        }
        if (Input.GetKey("a") && !player.IsSleeping() )
        {
            if (onGround)
            {
                if (player.velocity.magnitude < maxSpeed)
                    {
                        player.AddForce(leftRun);
                    }
            }
            else
            {
                {
                    player.AddForce(leftGlide);
                }
            }       
        }
    }
}
