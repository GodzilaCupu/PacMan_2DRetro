using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        PLAY, PACMAN_DYING, PACMAN_DEAD, GAME_OVER, GAME_WON
    };
    public GameState gameState = GameState.PLAY;

    public Image gameWonScreen;
    public Image gameOverScreen;

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
        gameOverScreen.enabled = false;
        gameWonScreen.enabled = false;
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
                    playerController.setLivesLeft(playerController.livesLeft - 1);
                    if (playerController.livesLeft >= 0)
                    {
                        playerController.setAlive(true);
                    }
                    else
                    {
                        gameState = GameState.GAME_OVER;
                    }
                    pacman.transform.position = Vector2.zero;
                    foreach (GameObject ghost in ghosts)
                    {
                        ghost.GetComponent<GhostController>().reset();
                    }
                }
                break;
            case GameState.GAME_OVER:
                gameOverScreen.enabled = true;
                gameWonScreen.enabled = false;
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
