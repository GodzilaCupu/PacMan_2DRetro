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
    [Range(1,10)]
    public float ghostVulnerableDuration = 2.0f;//how long the ghosts should be vulnerable for
    
    public Image gameWonScreen;
    public Image gameOverScreen;

    public GameObject pacman;
    public AnimationClip pacmanDeathAnimation;
    public List<GameObject> ghosts;
    public List<GameObject> pills;

    private static GameManager instance;
    private float respawnTime;
    private float invulnerableTime = 0;//when the ghosts will become invulnerable again

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
            case GameState.PLAY:
                bool foundPill = false;
                foreach (GameObject pill in pills)
                {
                    if (pill.activeSelf)
                    {
                        foundPill = true;
                        break;
                    }
                }
                if (!foundPill)
                {
                    gameState = GameState.GAME_WON;
                }
                break;
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
                if (Input.anyKeyDown)
                {
                    resetGame();
                    gameState = GameState.PLAY;
                    gameOverScreen.enabled = false;
                    gameWonScreen.enabled = false;
                }
                break;
            case GameState.GAME_WON:
                gameOverScreen.enabled = false;
                gameWonScreen.enabled = true;
                if (Input.anyKeyDown)
                {
                    resetGame();
                    gameState = GameState.PLAY;
                    gameOverScreen.enabled = false;
                    gameWonScreen.enabled = false;
                }
                break;
        }
        //Ghost Vulnerability
        if (invulnerableTime > 0)
        {
            if (Time.time > invulnerableTime)
            {
                foreach (GameObject ghost in ghosts)
                {
                    ghost.GetComponent<GhostController>().setVulnerable(false);
                }
            }
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

    public void resetGame()
    {
        pacman.transform.position = Vector2.zero;
        PlayerController playerController = pacman.GetComponent<PlayerController>();
        playerController.setLivesLeft(2);
        playerController.setAlive(true);
        foreach (GameObject ghost in ghosts)
        {
            ghost.GetComponent<GhostController>().reset();
        }
        foreach (GameObject pill in pills)
        {
            pill.SetActive(true);
        }
    }

    public static void makeGhostsVulnerable()
    {
        instance.invulnerableTime = Time.time + instance.ghostVulnerableDuration;
        foreach (GameObject ghost in instance.ghosts)
        {
            ghost.GetComponent<GhostController>().setVulnerable(true);
        }
    }
}
