/*  Player Movement Script
        Takes care of Moving Player with Input and Firing Projectiles
    2017 - Brady O'Leary
*/
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject projectile = null;
    public Vector3 projOffset = new Vector3(0, 0, 0);

    //Fire Cool Down Time in Seconds
    public float coolDown = 0.01f;
    private float lastCoolDownTime = 0;
    
    public float moveModif = 10.0f;
    public float deadzone = 0.001f;
    
    //Movement Restrictions for Vertical Axis
    public float upperBound = 5;
    public float lowerBound = -5;
    
    //Movement Restrictions for Horizontal Axis
    public float startPoint = 0;
    
    private BattleStats stats;
    
	// Use this for initialization
	void Start () {
        stats = gameObject.GetComponent<BattleStats>();
	}
	
	// Update is called once per frame
	void Update () {
        
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        
        if ((Mathf.Abs(horiz) >= deadzone || Mathf.Abs(vert) >= deadzone) && !stats.IsDead()) {
            if ((gameObject.transform.position.y < lowerBound && vert < 0.0) || (gameObject.transform.position.y > upperBound && vert > 0.0)) {
                //Vert Position is bad
                vert = 0.0f;
            }
            if (gameObject.transform.position.x < startPoint && horiz < 0.0) {
                //Horiz Position is bad
                horiz = 0.0f;
            }
            transform.position += new Vector3(horiz * moveModif * Time.deltaTime, vert * moveModif * Time.deltaTime, 0);
        }
        
        if (Input.GetButton("Fire1") && Time.time > lastCoolDownTime + coolDown && !stats.IsDead()) {
            lastCoolDownTime = Time.time;
            Instantiate (projectile, gameObject.transform.position + projOffset, Quaternion.identity);
            projectile.tag = gameObject.tag;
            projectile.GetComponent<ProjectileMovement>().dir = ProjectileMovement.Direction.Right;
            //projectile.transform.position = gameObject.transform.position + projOffset;
        }
	}
}
