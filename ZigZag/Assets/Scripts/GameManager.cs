using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public bool gameStarted;
    public GameObject platformSpawner,gamePlayUI,menuUI;
    public int score = 0,highScore;
    public Text scoreText,highScoreText;
    public AudioClip[] gameMusic; 

    AudioSource audioSource;  

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best Score : " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart(); 
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameStart()
    {
        
        gameStarted = true;
        platformSpawner.SetActive(true);

        menuUI.SetActive(false);
        gamePlayUI.SetActive(true);

        audioSource.clip = gameMusic[1];
        audioSource.Play();

        StartCoroutine("UpdateScore");
        StartCoroutine("IncSpeed");

    }

    public void GameOver()
    {
        platformSpawner.SetActive(false);
        gameStarted = false;
        StopCoroutine("UpdateScore");
        SaveHighScore();
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score++;
            scoreText.text = score.ToString();
        }
    }

    
    IEnumerator IncSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(25f);
            CarController.moveSpeed += 0.5f;
        }
    } 

    void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if(score> PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
