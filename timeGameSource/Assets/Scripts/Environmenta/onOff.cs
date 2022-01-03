using UnityEngine;
using System.Collections;

public class onOff : MonoBehaviour
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
