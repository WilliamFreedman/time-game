using UnityEngine;

public class FrozenTimer : MonoBehaviour
{
    // Start is called before the first frame update

    
    
    public float frozenMax = 5f; //Max amount of frozen juice
    public float frozenTimer = 5f; //Amount of frozen juice left

    public float rechargeRate = .5f; //How fast frozen juice recharges when not frozen (per second)
    public float drainRate = 1f; //How fast frozen juice depletes when frozen (per second)
    public timeStop getFrozen;

    public void fill() 
    {
        frozenTimer = frozenMax;//sets current juice value to max
    }

    // Update is called once per frame
    void Update()
    {
        if(getFrozen.frozen) { //If frozen, deplete juice by drainRate
            frozenTimer -= drainRate * Time.deltaTime;
            if(frozenTimer <= 0) { //If we're out of juice, unfreeze
                frozenTimer = 0; //Ensures frozenTimer does not become negative
                getFrozen.UnFreeze();
            }
        }
        else if(frozenTimer < frozenMax) {
            frozenTimer += rechargeRate * Time.deltaTime;
            if(frozenTimer > frozenMax) { //Ensures frozenTimer does not go above teh cap
                frozenTimer = frozenMax;
            }
        }
    }
}
