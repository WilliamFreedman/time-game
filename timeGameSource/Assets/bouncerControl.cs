using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncerControl : MonoBehaviour
{
    
    public Rigidbody2D self;
    public Collider2D leftBound;
    public Collider2D rightBound;
    public float speed = 7;
    
    // Start is called before the first frame update
    void Start()
    {
    
        Vector3 mag = new Vector3(7,0,0);
        Vector3 targetVelocity = self.transform.right * speed;
        self.velocity = new Vector2(targetVelocity.x,targetVelocity.y);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == leftBound || other == rightBound)
        {
            self.velocity *= -1;
        }

    }
}
