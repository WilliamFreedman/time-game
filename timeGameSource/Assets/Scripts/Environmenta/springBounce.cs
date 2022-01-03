using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springBounce : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D other) 
    {
        other.collider.attachedRigidbody.AddForce(new Vector3(25,35,0),ForceMode2D.Impulse);
    }
}
