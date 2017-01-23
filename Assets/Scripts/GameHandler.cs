using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    
    private GameObject player;
    private BattleStats playerStats;
    private GameObject healthTextBox;
    
    private Text txt;
    
    private float nextGame = 0.0f;
    private bool reset = false;
    private bool moveOn = false;
    
    private GameObject gen;
    private LevelGeneratorScript lgs;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<BattleStats>();
        txt = gameObject.GetComponent<Text>();
        gen = GameObject.Find("LevelGenerator");
        lgs = gen.GetComponent<LevelGeneratorScript>();
	}
	
	// Update is called once per frame
	void Update () {
        
        //print (lgs.levelIsFinished);
        
        if (playerStats.IsDead() && !reset) {
            txt.text = "Game Over";
            reset = true;
            nextGame = Time.time + 2.5f;
        }
        
        if (lgs.levelIsFinished && !moveOn) {
            lgs.waveNumber++;
            txt.text = "Wave " + lgs.waveNumber;
            moveOn = true;
            nextGame = Time.time + 2.5f;
        }
        
        if (reset && nextGame < Time.time) {
            lgs.GenerateNewLevel();
            playerStats.health = 100;
            moveOn = false;
            reset = false;
        }
        
        if (moveOn && nextGame < Time.time) {
            lgs.GenerateNewLevel();
            playerStats.health = 100;
            moveOn = false;
            reset = false;
        }
	
	}
}
