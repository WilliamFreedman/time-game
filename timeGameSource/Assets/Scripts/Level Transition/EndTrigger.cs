using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("player"))
            gameManager.CompleteLevel();
    }
}
