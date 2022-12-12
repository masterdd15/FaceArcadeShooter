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
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Press R to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTheGame();
        }
        SetScore();


    }

    void SetScore()
    {
        scoreText.text = "Score: " + score;
    }

    void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("The scene has reset");
    }
}
