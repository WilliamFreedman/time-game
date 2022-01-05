using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setToZero : MonoBehaviour
{
    public Transform self;
    // Start is called before the first frame update
    void Start()
    {
        self.position = new Vector3(0,0,0);
    }

}
