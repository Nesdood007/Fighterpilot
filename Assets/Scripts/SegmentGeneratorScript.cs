/*  This Generates a Segment based on Variable Input

*/

using UnityEngine;
using System.Collections;

public class SegmentGeneratorScript : MonoBehaviour {
    
    private bool isFinished = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isFinished) Destroy(this.gameObject);
	}
    
    //Generate a Segment: Circle dev into segments based on difficulty
    public void Generate(int difficulty, GameObject enemy, Vector3 pos) {
        Vector3 position;
        float radius = (difficulty % 5) + 1;
        if (difficulty <= 0) difficulty = 1;
        float degrees = 360 / difficulty;
        //print (degrees + " " + radius);
        for (int i = 0; i < difficulty; i++) {
            position = pos + new Vector3(Mathf.Cos(degrees * i * Mathf.Deg2Rad) * radius, Mathf.Sin(degrees * i * Mathf.Deg2Rad) * radius, 0);
            print (pos);
            Instantiate (enemy, position, Quaternion.identity);
        }
        isFinished = true;
    }
}
