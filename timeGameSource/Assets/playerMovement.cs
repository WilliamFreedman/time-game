using UnityEngine;

public class playerMovement : MonoBehaviour
{

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown("Space"))
        {
            Debug.Log("jumping");
        }
    }
}
