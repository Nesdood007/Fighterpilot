/*  Updates the Text in the Text box with Player Health Info

*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {
    
    private GameObject player;
    private BattleStats playerStats;
    private GameObject healthTextBox;
    
    private Text txt;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<BattleStats>();
        txt = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        int health = playerStats.health;
        txt.text = "Health: " + health;
        
	}
}
