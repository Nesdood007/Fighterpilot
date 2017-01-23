/*  Stats Script: 
        Stores info on HP, POW, DEF, SPC, SHIELD, and initial Values of all of those.

*/
using UnityEngine;
using System.Collections;

public class BattleStats : MonoBehaviour {
    
    public int maxHealth = 100;
    public int maxPower = 5;
    public int maxDefense = 1;
    public int maxSpecial = 20;
    public int maxShield = 5;
    
    public int health;
    public int power;
    public int defense;
    public int special;
    public int shield;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        power = maxPower;
        defense = maxDefense;
        special = maxSpecial;
        shield = maxShield;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    // Do Damage to this Object
    public void DoDamage(int damage) {
        health -= (damage - defense > 0) ? damage - defense : 0;
    }
    
    // Returns True if Health is less than zero
    public bool IsDead() {
        return health <= 0;
    }
}
