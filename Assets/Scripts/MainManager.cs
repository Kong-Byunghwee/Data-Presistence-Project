using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public static MainManager Instance;
    public Text ScoreText;
    public Text BestScoreText;
    private int currentScore = 0;
    public int H_Score =0;
    public GameObject GameOverText;
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    
   
    void Start()
    {
        if(PlayerPrefs.HasKey("High_Score")){
            H_Score = PlayerPrefs.GetInt("High_Score");
        }
        DisplayBestScore();
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
        //ResetScore();
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        currentScore = m_Points;
        SaveUserScore();
    }

    /*public void HighScore(){
        if(bestScore <= m_Points){
            PlayerPrefs.SetInt(ScoreKeyName, m_Points);
            bestScoreUser = MenuManager.Instance.userName;
            PlayerPrefs.SetString(UserKeyName, bestScoreUser);
        }
    }*/

    /*public void ResetScore(){
        if(Input.GetKeyDown(KeyCode.G)){
            PlayerPrefs.SetInt(ScoreKeyName, 0);
            PlayerPrefs.SetString(UserKeyName, "None");
            DisplayBestScore();
        }
    }*/

    public void DisplayBestScore(){
        if(PlayerPrefs.HasKey("High_Player")){
            string H_User = PlayerPrefs.GetString("High_Player");
            int H_Score = PlayerPrefs.GetInt("High_Score");
            BestScoreText.text = $"Best Score : {H_User} : {H_Score}";
        }
        
    }

    public void SaveUserScore(){
        if(H_Score < currentScore){
            string H_User = PlayerPrefs.GetString("Cur_Player");
            PlayerPrefs.SetString("High_Player", H_User);
            PlayerPrefs.SetInt("High_Score", currentScore);
        }
        
    }

}
