
using UnityEngine;
// Shot not fixing the gravity jump bug - Will
public class timeStop : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool frozen = false;

    public int gravValue = 1; // this is where we should edit gravity scale for the player, makes the scripting a lot easier

    Vector3 currentVelocity = new Vector3(0,0,0);
    // Update is called once per frame

    void Start()
    {
        rb.gravityScale = gravValue;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && frozen)
        {
            rb.AddForce(currentVelocity*rb.mass,ForceMode2D.Impulse);
            rb.gravityScale = gravValue;
            frozen = false;
        }

        else if (Input.GetKeyDown("space") && !frozen)
        {
            currentVelocity = rb.velocity;
            rb.velocity = new Vector3(0,0,0); 
            rb.gravityScale = 0;
            frozen = true;
        }

        
    }

}
