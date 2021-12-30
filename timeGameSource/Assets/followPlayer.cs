using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.2f;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;




    // Update is called once per frame
    void Update()
    {
        cameraPos = new Vector3(player.position.x,player.position.y,-10f);
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, cameraPos, ref velocity, dampTime);
    }
}
