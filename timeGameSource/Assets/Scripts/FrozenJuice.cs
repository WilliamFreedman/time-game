using UnityEngine;
using UnityEngine.UI; 

public class FrozenJuice : MonoBehaviour
{
    // Start is called before the first frame update
    public FrozenTimer freezeTime;
    public Image juiceBar; //The bar image we're manipulating


    // Update is called once per frame
    void Update()
    {
        juiceBar.fillAmount = freezeTime.frozenTimer/freezeTime.frozenMax; 
        //fillAmount takes a value from (0, 1) --the percent filled
        //Set the percent equal to the current juice level (frozenTimer) divided by the max juice level (frozenMax)
    }

}
