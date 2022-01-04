using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onOffControl : MonoBehaviour
{
    public GameObject laser;
    
    void flicker()
    {
        laser.SetActive(!laser.activeSelf);
    }

    void Start()
    {
        InvokeRepeating("flicker", 0f, .8f);
    }


}
