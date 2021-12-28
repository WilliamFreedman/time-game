using UnityEngine;

public class FrozenTimer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float frozenTimer = 5f;

    public float rechargeRate = .5f;
    public float drainRate = 1f;
    public timeStop getFrozen;

    // Update is called once per frame
    void Update()
    {
        if(getFrozen.frozen) {
            frozenTimer -= drainRate * Time.deltaTime;
            if(frozenTimer <= 0) {
                frozenTimer = 0;
                getFrozen.UnFreeze();
            }
        }
        else if(frozenTimer < 5) {
            frozenTimer += rechargeRate * Time.deltaTime;
            if(frozenTimer > 5) {
                frozenTimer = 5;
            }
        }
    }
}
