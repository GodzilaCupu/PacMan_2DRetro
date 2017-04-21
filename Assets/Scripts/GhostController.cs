using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    public float speed = 1.0f;//the speed this ghost can travel
    public Vector2 direction = Vector2.up;//the direction this ghost is going

    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = direction * speed;
        if (rb2d.velocity.x == 0)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        if (rb2d.velocity.y == 0)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
        }
    }
}
