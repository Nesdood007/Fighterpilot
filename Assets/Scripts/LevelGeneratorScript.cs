/*  This will Generate Levels in Fighterpilot based on the wave number
        Wave 0 will have easy and sparse enemies
        Levels have defined MaxLength
        
        How Levels are Generated:
            Level length and difficulty based on wave number: 
            Generation broken into segments each with random difficulty

*/

using UnityEngine;
using System.Collections;

public class LevelGeneratorScript : MonoBehaviour {
    
    public int waveNumber = 0;
    
    //Maximum Space Between Sections
    public int maxSpace = 15;
    public int minSpace = 10;
    
    public int maxDifficulty = 1;
    
    //Point to start Generating Level
    public Vector3 startingPoint = new Vector3(20,0,0);
    
    //Segment Generator Object
    public GameObject segmentGen;
    private SegmentGeneratorScript sgs;
    
    public GameObject[] enemiesToSpawn;
    
    private bool levelIsGen = false;
    
    //Set to true when Player passes the Level Finish Line.
    public bool levelIsFinished = false;
    public float finishLine;
    private GameObject player;
    //Generate Level at Game Start
    public bool genLevelAtStart = true;

	// Use this for initialization
	void Start () {
        sgs = segmentGen.GetComponent<SegmentGeneratorScript>();
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
    
    public void GenerateNewLevel() {
        
        Vector3 currPoint = startingPoint;
        player.transform.position = startingPoint;
        int localDiff = waveNumber;
        if (waveNumber > maxDifficulty) localDiff = waveNumber % maxDifficulty;
        for (int i = 0; i < waveNumber + 1; i++) {
            Instantiate (segmentGen, currPoint, Quaternion.identity);
            sgs.Generate(Random.Range(0, localDiff), enemiesToSpawn[Random.Range(0,enemiesToSpawn.Length)], currPoint);
            currPoint += new Vector3((int)Random.Range(minSpace, maxSpace), 0, 0);
        }
        finishLine = currPoint.x;
        print ("Finish: " + finishLine);
    }
}
