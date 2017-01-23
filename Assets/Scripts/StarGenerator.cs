/*  Star Generator for Cool Background
        Add in hue variation later

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarGenerator : MonoBehaviour {
    
    public GameObject star;
    
    public float speed = 10;
    
    //Set to -1 or 1
    //public int direction = -1;
    
    //Maximum Number of Stars, Both must be greater than zero
    public int maxStars = 1000;
    public int maxScale = 2;
    
    //Relative Length of the stars
    public float relLength = 1.0f;
    
    //Spawn Positions.
    private float horizSpawn;
    private float horizDespawn;
    private float screenTop;
    private float screenBottom;
    
    private List<GameObject> stars = new List<GameObject>();
    //private int[] starlengths;

	// Use this for initialization
	void Start () {
        horizSpawn = Camera.main.orthographicSize * Screen.width / Screen.height;
        horizDespawn = -(Camera.main.orthographicSize * Screen.width / Screen.height);
        screenTop = Camera.main.orthographicSize;
        screenBottom = -Camera.main.orthographicSize;
        //print (horizSpawn + " " + horizDespawn + " " + screenTop + " " + screenBottom);
        GenStars();
	}
	
	// Update is called once per frame
	void Update () {
        float scale, localSpeed;
        
        for (int i = 0; i < stars.Count; i++) {
            scale = stars[i].transform.localScale.y;
            localSpeed = scale / maxScale;
            //print (scale + " " + localSpeed);
            stars[i].transform.position -= new Vector3(localSpeed * speed, 0, 0);
            if (stars[i].transform.position.x < horizDespawn + gameObject.transform.position.x - (scale * relLength)) {
                Destroy (stars[i]);
                stars.RemoveAt(i);
                i--;
            }
        }
        
        //Adds a few more stars
        for(int i = (int)Random.Range(0, (float) maxStars / 2);i > 0 && stars.Count < maxStars; i--) {
            GameObject temp = Instantiate (star);
            scale = Random.Range(0.0f, maxScale);
            //print (scale);
            temp.transform.localScale = new Vector3(scale * relLength, scale, 0);
            temp.transform.position = new Vector3(horizSpawn + gameObject.transform.position.x + (scale * relLength), Random.Range(screenTop, screenBottom), 0);
            //print ("Added Star");
            stars.Add(temp);
        }
	
	}
    
    //Generates some Stars
    public void GenStars() {
        float scale = 0.0f;
        for (int i = 0; i < maxStars / 4; i++) {
            GameObject temp = Instantiate (star);
            scale = Random.Range(0.0f, maxScale);
            //print (scale);
            temp.transform.localScale = new Vector3(scale * relLength, scale, 0);
            temp.transform.position = new Vector3(horizSpawn + gameObject.transform.position.x, Random.Range(screenTop, screenBottom), 0);
            //print ("Added Star");
            stars.Add(temp);
        }
    }

}
