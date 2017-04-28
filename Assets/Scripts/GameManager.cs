using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        PLAY, PACMAN_DYING, PACMAN_DEAD, GAME_OVER, GAME_WON
    };
    public GameState gameState = GameState.PLAY;

    public GameObject pacman;

    private static GameManager instance;

	// Use this for initialization
	void Start () {
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void pacmanKilled()
    {
        instance.pacman.GetComponent<PlayerController>().setAlive(false);
        instance.gameState = GameState.PACMAN_DYING;
    }
}
