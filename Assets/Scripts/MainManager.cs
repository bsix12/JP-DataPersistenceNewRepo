using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;              // reference to highScoreText on canvas.  ADD REFERENCE in inspector
    public TextMeshProUGUI gameOverText;
    public TMP_InputField playerInputField;
    // public GameObject gameOverText;                  // replaced with TMP
    
    private bool m_Started = false;
    private int currentScore;
    private int highScore = 0;              // container to store the highScore
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                playerInputField.gameObject.SetActive(false);
                highScoreText.text = "High Score: " + currentScore + " by " + playerInputField.text;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void CheckIfHighScore()                 // compare current score to highScore
    {
        if (currentScore > highScore)
        {
            GetPlayerInput();
                        
        }
    }

    void GetPlayerInput()
    {
        playerInputField.gameObject.SetActive(true);
    }

    void AddPoint(int point)
    {
        currentScore += point;
        scoreText.text = $"Score : {currentScore}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        gameOverText.gameObject.SetActive(true);
        CheckIfHighScore();                 // check if new high score
    }
    

}
