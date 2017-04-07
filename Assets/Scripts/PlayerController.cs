using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;//the speed pacman can travel

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb2d.velocity = Vector2.left * speed;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb2d.velocity = Vector2.right * speed;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            rb2d.velocity = Vector2.down * speed;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            rb2d.velocity = Vector2.up * speed;
        }
    }
}
