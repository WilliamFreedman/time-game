using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncer : MonoBehaviour
{

    public Rigidbody2D self;
    public Collider2D leftBound;
    public Collider2D rightBound;
    // Start is called before the first frame update
    void Start()
    {
        self.isKinematic = false;
        self.velocity = new Vector3(7,0,0);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hi");
        self.velocity *= -1;
    }
}
