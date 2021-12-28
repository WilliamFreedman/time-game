using UnityEngine;
using UnityEngine.UI; 

public class FrozenJuice : MonoBehaviour
{
    // Start is called before the first frame update
    public FrozenTimer freezeTime;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = freezeTime.frozenTimer.ToString();
    }

}
