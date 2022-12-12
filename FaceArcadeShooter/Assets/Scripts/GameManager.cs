using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    public int score;
    [SerializeField] TextMeshProUGUI scoreText;

    public int highscore;
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //score = 0;
        //scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        //highScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) //We are in the first menu
        {

        }
        
        //Press R to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTheGame();
        }
        SetScore();
        SetHighScore();
    }

    void SetHighScore()
    {
        if(score > highscore)
        {
            highscore = score;
        }
        highScoreText.text = "High Score: " + highscore;
    }

    void SetScore()
    {
        if(scoreText == null)
        {
            score = 0;
            scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
            highScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<TextMeshProUGUI>();
        }
        scoreText.text = "Score: " + score;
    }

    void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("The scene has reset");
    }
}
