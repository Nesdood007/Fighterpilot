/*  Enemy Movement Script
        Moves the enemy and destroys it when it is dead

*/

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public GameObject projectile = null;
    public Vector3 projOffset = new Vector3(0, 0, 0);

    //Fire Cool Down Time in Seconds
    public float coolDown = 0.05f;
    private float lastCoolDownTime = 0;
    
    public float moveModif = 10.0f;
    
    private BattleStats stats;
    
    //Player reference is ised for basic AI
    private GameObject player;
    
    //Distance before Enemy Engages Player
    public float distToEngage = 3.0f;
    private bool isFiring = false;
    private bool isTracking = false;
    
	// Use this for initialization
	void Start () {
        stats = this.gameObject.GetComponent<BattleStats>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //Do This later
        if (stats.IsDead()) {
            Destroy (this.gameObject);
        }
        
        //AI Functions
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < distToEngage) {
            isFiring = isTracking = true;
        } else {
            isFiring = isTracking = false;
        }
        
        //Fires Projectile if Firing
        if (isFiring && Time.time > lastCoolDownTime + coolDown) {
            lastCoolDownTime = Time.time;
            Instantiate (projectile, gameObject.transform.position + projOffset,Quaternion.identity);
            projectile.tag = gameObject.tag;
            projectile.GetComponent<ProjectileMovement>().dir = ProjectileMovement.Direction.Left;
            //projectile.transform.position = transform.position + projOffset;
        }
        
        //Movement Portion
        float horiz = 0.0f;
        float vert = 0.0f;
        
        if (isTracking) {
            if (player.transform.position.y < gameObject.transform.position.y) {
                horiz = -0.5f;
            } else if (player.transform.position.y > gameObject.transform.position.y) {
                horiz = 0.5f;
            }
            
        } else {
            
            int degrees = (int) (Time.time * 100 % 360);
            horiz = -1.0f;
            vert = Mathf.Cos(Mathf.Deg2Rad * degrees);
            //print (vert);
        }
        
        if (horiz != 0.0f || vert != 0.0f) {
            gameObject.transform.position += new Vector3(horiz * moveModif * Time.deltaTime, vert * moveModif * Time.deltaTime, 0);
        }
        
	}
}
