
using UnityEngine;
// Shot not fixing the gravity jump bug - Will
public class timeStop : MonoBehaviour
{
    public Rigidbody2D rb; //reference to player's rigidbody

    public bool frozen = false; //if the player is frozen

    private float gravValue; // this is where we should edit gravity scale for the player, makes the scripting a lot easier

    Vector3 currentVelocity = new Vector3(0,0,0); // stores player's velocity, gets updated whenever the player wants to stop time. initialized as {0,0,0} because c# does weird things with non static fields

    public GrapplingGun grapple = null;
    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.collider.gameObject.tag == "pusher" && frozen)
        {
            rb.velocity = new Vector3(0,0,0);
        }
    }

    void Start()
    {
        gravValue = rb.gravityScale; // sets player's gravity value at the start of the game, should this be changed to active if were instantiating prefabs?
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && frozen) // when freeze button is pressed, checks if the character is already frozen
        { //if the character is already in the frozen position
            UnFreeze();
        }

        else if (Input.GetKeyDown("space") && !frozen) //if not already frozen
        {
            Freeze();
        }

        
    }
    public void Freeze() { //Freezes the player
        currentVelocity = rb.velocity; //saves velocity in currentVelocity
            rb.velocity = new Vector3(0,0,0); //freezes player
            rb.gravityScale = 0;//turns off gravity
            frozen = true;
    }

    public void UnFreeze() { //Unfreezes the player
        rb.AddForce(currentVelocity*rb.mass,ForceMode2D.Impulse); //adds an impulse to put the player back on the path they were on
            rb.gravityScale = gravValue;//resets gravity
            frozen = false;
            if(!Input.GetKey(KeyCode.Mouse0)&&grapple!=null)
                grapple.wasFrozen = true;
    }

}
