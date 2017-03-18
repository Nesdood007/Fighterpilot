/*  General Collision Script
        Will deal damage to other object if the tag is not the same

*/

using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
    
    private BattleStats stats;

	// Use this for initialization
	void Start () {
        stats = this.gameObject.GetComponent<BattleStats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D coll) {
        //print (coll);
        if (coll.tag != gameObject.tag) {
            BattleStats bs = coll.GetComponent<BattleStats>();
            if (bs != null) {
                bs.DoDamage(stats.power);
                stats.DoDamage(bs.power);
            }
        }
    }
}
