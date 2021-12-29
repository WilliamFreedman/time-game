using UnityEngine;
using UnityEngine.UI; 

public class FrozenJuice : MonoBehaviour
{
    // Start is called before the first frame update
    public FrozenTimer freezeTime;
    public Image juiceBar;
    void Setup() {
        juiceBar.fillCenter = true;
    }


    // Update is called once per frame
    void Update()
    {
        juiceBar.fillAmount = freezeTime.frozenTimer/freezeTime.frozenMax;
    }

}
