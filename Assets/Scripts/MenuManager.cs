using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters;
using Microsoft.SqlServer.Server;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public InputField inputName;
    public string currentName = null;
    public TextMeshProUGUI NowBestScore;
    void Awake(){
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance =  this;
        DontDestroyOnLoad(gameObject);

        MenuBestScoreText();
    }

    public void StartNew(){
        if(PlayerPrefs.HasKey("Cur_Player")){
            SceneManager.LoadScene(1);
        }
    }

    public void Exit(){
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
    
    public void SaveUserData(){
        currentName = inputName.text;
        PlayerPrefs.SetString("Cur_Player", currentName);
        string PlayerNow = PlayerPrefs.GetString("Cur_Player");
        Debug.Log($"Cur_Player : {PlayerNow}");
    }

    public void MenuBestScoreText(){
        if(PlayerPrefs.HasKey("High_Player")){
            string Top_User = PlayerPrefs.GetString("High_Player");
            int Top_Score = PlayerPrefs.GetInt("High_Score");
            NowBestScore.text = $"BestScore : {Top_User} : {Top_Score}";
        }
        
    }

}

