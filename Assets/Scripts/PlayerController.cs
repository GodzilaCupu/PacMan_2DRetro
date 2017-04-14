using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;//the speed pacman can travel
    public int score = 0;//the score

    public Text scoreText;//the Text UI Component that shows the score

    private Vector2 direction;//the direction pacman is going

    Rigidbody2D rb2d;
    Animator animator;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        animator.SetFloat("currentSpeed", rb2d.velocity.magnitude);
        if (Input.GetAxis("Horizontal") < 0)
        {
            direction = Vector2.left;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            direction = Vector2.right;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            direction = Vector2.down;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            direction = Vector2.up;
        }
        rb2d.velocity = direction * speed;
        transform.up = direction;
        if (rb2d.velocity.x == 0)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        if (rb2d.velocity.y == 0)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
        }
    }

    public void addPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = ""+score;
    }
}
