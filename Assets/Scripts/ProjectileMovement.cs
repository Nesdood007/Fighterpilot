/*  Projectile Movement Script Component
        Moves the Projectile and destroys it after the Lifetime expires.
*/

using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {

	public float moveModif = 10.0f;
	public enum Direction {Left = -1, Zero, Right};
    public Direction dir = Direction.Zero;
    
    public Sprite left;
    public Sprite right;
    
    //Lifetime in Seconds
    public float lifetime = 100.0f;
    private float startTime = 0.0f;
    
    private SpriteRenderer rend;
    
    private BattleStats stats;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        startTime = Time.time;
        stats = gameObject.GetComponent<BattleStats>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(((int) dir) * moveModif * Time.deltaTime, 0, 0);
        if (dir == Direction.Left) {
            rend.sprite = left;
        } else {
            rend.sprite = right;
        }
        if (Time.time > startTime + lifetime || stats.IsDead()) {
            Destroy(gameObject);
        }
	}
}
