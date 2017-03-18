/*  This will Generate Levels in Fighterpilot based on the wave number
        Wave 0 will have easy and sparse enemies
        Levels have defined MaxLength
        
        How Levels are Generated:
            Level length and difficulty based on wave number: 
            Generation broken into segments each with random difficulty

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorScript : MonoBehaviour {
    
    public int waveNumber = 0;
    
    //Maximum Space Between Sections
    public int maxSpace = 15;
    public int minSpace = 10;
    
    public int maxDifficulty = 1;
    
    //Point to start Generating Level
    public Vector3 startingPoint = new Vector3(20, 0, 0);
    
    //All Enemies that can be spawned into the game
    public GameObject[] enemiesToSpawn;
    
    private bool levelIsGen = false;
    
    //Set to true when Player passes the Level Finish Line.
    public bool levelIsFinished = false;
    public float finishLine;
    private GameObject player;
    
    //Generate Level at Game Start
    public bool genLevelAtStart = true;
    
    //List of Generated Enemies, makes deleting them easier
    private List<GameObject> enemiesToDelete = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //sgs = segmentGen.GetComponent<SegmentGeneratorScript>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if ((player.transform.position.x > startingPoint.x || genLevelAtStart) && !levelIsGen) {
            levelIsGen = true;
            GenerateNewLevel();
        }
        if (player.transform.position.x > finishLine && !levelIsFinished) {
            levelIsFinished = true;
            //print ("Level Finished!");
        }
	}
    
    //Generates a new Level/Wave
    public void GenerateNewLevel() {
        levelIsFinished = false;
        gameObject.transform.position = new Vector3(player.transform.position.x, 0, 0);
        Vector3 currPoint = gameObject.transform.position + startingPoint;
        print (currPoint);
        //player.transform.position = startingPoint + gameObject.transform.position;
        int localDiff = waveNumber;
        if (waveNumber > maxDifficulty) localDiff = waveNumber % maxDifficulty;
        for (int i = 0; i < waveNumber + 1; i++) {
            GenerateSegment(Random.Range(0, localDiff), enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], currPoint);
            currPoint += new Vector3((int)Random.Range(minSpace, maxSpace), 0, 0);
        }
        finishLine = currPoint.x;
        print ("Finish: " + finishLine);
    }
    
    //Generates a Segment of the level
    //  Difficulty = Number of Enemies to Spawn
    //  enemy = Enemy to Spawn
    //  pos = Position for center of spawn circle
    private void GenerateSegment(int difficulty, GameObject enemy, Vector3 pos) {
        Vector3 position;
        float radius = (difficulty % 5) + 1;
        if (difficulty <= 0) difficulty = 1;
        float degrees = 360 / difficulty;
        //print (degrees + " " + radius);
        for (int i = 0; i < difficulty; i++) {
            position = pos + new Vector3(Mathf.Cos(degrees * i * Mathf.Deg2Rad) * radius, Mathf.Sin(degrees * i * Mathf.Deg2Rad) * radius, 0);
            print (pos);
            GameObject inst = Instantiate (enemy, position, Quaternion.identity) as GameObject;
            enemiesToDelete.Add(inst);
        }
    }
    
    //Destroy a Level
    public void DestroyLevel() {
        foreach (GameObject temp in enemiesToDelete) {
            print (temp);
            if (temp != null) {
                Destroy (temp);
            }
        }
        enemiesToDelete.Clear();
    }
    
    //Reset the Game
    public void ResetGame() {
        DestroyLevel();
        waveNumber = 0;
        levelIsGen = false;
        levelIsFinished = false;
        gameObject.transform.position = startingPoint;
        player.transform.position = new Vector3(0, 0, 0);
    }
}
