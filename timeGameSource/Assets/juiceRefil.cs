using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juiceRefil : MonoBehaviour
{

    public FrozenTimer timerControls;

    void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.gameObject.tag == "player")
        { 
            timerControls.fill();//calls fill to set frozen juice meter to max
        }
    }
   // Update is called once per frame
    void Update()
    {
        
    }
}
