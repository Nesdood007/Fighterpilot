/*  Camera Movement Script
        Moves the Camera when the player is moving so that the camera stays on the player
*/

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    private GameObject player;
    
    public bool moveHorizontal = true;
    public bool moveBackwards = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x > gameObject.transform.position.x && moveHorizontal) {
            //print (gameObject.transform.position.x);
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        
        if (player.transform.position.x < gameObject.transform.position.x && moveBackwards && moveHorizontal) {
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
	}
}
