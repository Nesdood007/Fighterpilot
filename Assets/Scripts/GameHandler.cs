using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    
    private GameObject player;
    private BattleStats playerStats;
    private GameObject healthTextBox;
    private StarGenerator starGen;
    
    private Text txt;
    
    private float nextGame = 0.0f;
    private bool reset = false;
    private bool moveOn = false;
    
    private GameObject gen;
    private LevelGeneratorScript lgs;
    
    //Star Generator Default Settings
    private float defaultSpeed, defaultRelLength;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<BattleStats>();
        txt = gameObject.GetComponent<Text>();
        gen = GameObject.Find("LevelGenerator");
        lgs = gen.GetComponent<LevelGeneratorScript>();
        starGen = GameObject.Find("Star Generator").GetComponent<StarGenerator>();
        defaultSpeed = starGen.speed;
        defaultRelLength = starGen.relLength;
	}
	
	// Update is called once per frame
	void Update () {
        
        //print (lgs.levelIsFinished);
        
        if (playerStats.IsDead() && !reset) {
            lgs.waveNumber = 0;
            txt.text = "Game Over";
            reset = true;
            starGen.speed = 0;
            nextGame = Time.time + 2.5f;
        }
        
        if (lgs.levelIsFinished && !moveOn) {
            lgs.waveNumber++;
            txt.text = "Wave " + lgs.waveNumber;
            moveOn = true;
            starGen.speed = 1.0f;
            starGen.relLength = 5.0f;
            nextGame = Time.time + 2.5f;
        }
        
        if ((moveOn || reset) && nextGame < Time.time) {
            txt.text = "";
            lgs.GenerateNewLevel();
            playerStats.health = 100;
            starGen.speed = defaultSpeed;
            starGen.relLength = defaultRelLength;
            if (Random.Range(0,4) == 0) {
                starGen.lsd = true;
            } else {
                starGen.lsd = false;
            }
            
            if (reset) {
                starGen.DestroyStars();
                lgs.ResetGame();
            }
            moveOn = false;
            reset = false;
        }
	}
}
