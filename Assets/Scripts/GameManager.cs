using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Button playButton;
    public Text playerLives;
    public GameObject gameOver;
    public GameObject gameTittle;
    public TimerCounter timerCounter;
    public Joystick playerJoystick;
    public Button shootBtn;

    const int maxLives = 3;
    int lives;

    public int score = 000000;
    public Text scoreText;

    SpriteRenderer playerSprite;

    void Awake()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        player.enabled = false;

        gameOver.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        timerCounter.GetComponent<TimerCounter>().StopTimeCounter();
        playerJoystick.gameObject.SetActive(false);
        shootBtn.gameObject.SetActive(false);
    }

    public void Play()
    {
        gameOver.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        gameTittle.gameObject.SetActive(false);


        player.enabled = true;
        playerSprite.enabled = true;
        player.gameObject.SetActive(true);
        player.Respawn();

        player.transform.position = new Vector2(0, -1);
        playerJoystick.gameObject.SetActive(true);
        playerJoystick.ResetInput();
        shootBtn.gameObject.SetActive(true);
        player.StopShooting();


        lives = maxLives;
        FindAnyObjectByType<EnemySpawner>().spawnRate = 1.4f;
        FindAnyObjectByType<EnemySpawner>().StartSpawn();


        score = 0;


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        timerCounter.GetComponent<TimerCounter>().StartTimeCounter();


        /*
                    EnemyControll[] enemies = FindObjectsOfType<EnemyControll>();

                    for (int i = 0; i < enemies.Length; i++)
                    {
                        Destroy(enemies[i].gameObject);
                    }
                */

    }

    void Update()
    {
        Lives();
        Score();
    }

    void Lives()
    {
        playerLives.text = lives.ToString();
    }

    public void LoseLives()
    {
        lives--;

        if (lives <= 0)
        {
            timerCounter.GetComponent<TimerCounter>().StopTimeCounter();
            gameOver.gameObject.SetActive(true);
            playerJoystick.gameObject.SetActive(false);
            shootBtn.gameObject.SetActive(false);

            Invoke(nameof(HideGameOverAndSHowGameTittle), 3f);
            Invoke(nameof(PlayerDeath), 1f);
            Invoke(nameof(ShowPlayButton), 3f);
        }
    }

    void ShowPlayButton()
    {
        playButton.gameObject.SetActive(true);
    }

    void PlayerDeath()
    {
        player.gameObject.SetActive(false);
        FindAnyObjectByType<EnemySpawner>().StopSpawn();
    }

    void HideGameOverAndSHowGameTittle()
    {
        gameOver.gameObject.SetActive(false);
        gameTittle.gameObject.SetActive(true);
    }

    public void Score()
    {
        scoreText.text = score.ToString("D6");
    }

}
