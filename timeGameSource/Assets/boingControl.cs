using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boingControl : MonoBehaviour
{

    public Vector3 springVal = new Vector3(25,35,0); //just good values I know work, feel free to change in inspector for ur level
    public Rigidbody2D player;

    void OnCollisionEnter2D(Collision2D other) 
    {
        other.collider.attachedRigidbody.AddForce(springVal*player.mass,ForceMode2D.Impulse);
    }
}