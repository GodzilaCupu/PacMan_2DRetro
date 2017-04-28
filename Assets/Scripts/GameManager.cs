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
    public AnimationClip pacmanDeathAnimation;
    public List<GameObject> ghosts;

    private static GameManager instance;
    private float respawnTime;

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
		switch (gameState)
        {
            case GameState.PACMAN_DYING:
                if (Time.time > respawnTime)
                {
                    gameState = GameState.PACMAN_DEAD;
                    respawnTime = Time.time + 1;
                    pacman.SetActive(false);
                }
                break;
            case GameState.PACMAN_DEAD:
                if (Time.time > respawnTime)
                {
                    gameState = GameState.PLAY;
                    pacman.SetActive(true);
                    PlayerController playerController = pacman.GetComponent<PlayerController>();
                    playerController.setAlive(true);
                    playerController.setLivesLeft(playerController.livesLeft - 1);
                    pacman.transform.position = Vector2.zero;
                    foreach (GameObject ghost in ghosts)
                    {
                        ghost.GetComponent<GhostController>().reset();
                    }
                }
                break;
        }
	}

    public static void pacmanKilled()
    {
        instance.pacman.GetComponent<PlayerController>().setAlive(false);
        instance.gameState = GameState.PACMAN_DYING;
        instance.respawnTime = Time.time + instance.pacmanDeathAnimation.length;
        foreach (GameObject ghost in instance.ghosts)
        {
            ghost.GetComponent<GhostController>().freeze(true);
        }
    }
}
