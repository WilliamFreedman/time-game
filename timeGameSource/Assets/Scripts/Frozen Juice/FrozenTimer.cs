using UnityEngine;

public class FrozenTimer : MonoBehaviour
{
    // Start is called before the first frame update

    
    
    public float frozenMax = 5f; //Max amount of frozen juice
    public float frozenTimeLeft = 5f; //Amount of frozen juice left

    public float rechargeRate = .5f; //How fast frozen juice recharges when not frozen (per second)
    public float drainRate = 1f; //How fast frozen juice depletes when frozen (per second)
    public timeStop getFrozen;

    public void fill() 
    {
        frozenTimeLeft = frozenMax;//sets current juice value to max
    }

    // Update is called once per frame
    void Update()
    {
        if(getFrozen.frozen) { //If frozen, deplete juice
            DrainJuice();
        }
        else if(frozenTimeLeft < frozenMax) { //If not frozen and less than the max, recharge
            RechargeJuice();
        }
    }

    void DrainJuice() {
        frozenTimeLeft -= drainRate * Time.deltaTime; //Deplete juice by drainRate
        if(frozenTimeLeft <= 0) { //If we're out of juice, unfreeze
            frozenTimeLeft = 0; //Ensures frozenTimeLeft does not become negative
            getFrozen.UnFreeze();
        }
    }
    void RechargeJuice() {
        frozenTimeLeft += rechargeRate * Time.deltaTime; //Recharge juice by rechargeRate
        if(frozenTimeLeft > frozenMax) { //Ensures frozenTimeLeft does not go above the cap
            frozenTimeLeft = frozenMax;
        }
    }
}
