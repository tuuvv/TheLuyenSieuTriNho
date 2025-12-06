using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
     // 🔥 KEY CHUẨN DÙNG TRONG PLAYER PREFS
    public const string PLAYER1_SCORE_KEY = "PLAYER1_SCORE";
    public const string PLAYER2_SCORE_KEY = "PLAYER2_SCORE";

    public string player1Name = "Player 1";
    public string player2Name = "Player 2";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MakeInstance();
        InitScoresForTheFirstTime();
    }


    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitScoresForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartForTheFirstTime"))
        {
            PlayerPrefs.SetInt(PLAYER1_SCORE_KEY, 0);
            PlayerPrefs.SetInt(PLAYER2_SCORE_KEY, 0);

            PlayerPrefs.SetInt("IsGameStartForTheFirstTime", 1);
        }
    }
    public void SetPlayerNames(string p1, string p2)
    {
        player1Name = p1;
        player2Name = p2;
    }

    // 🔥 TĂNG ĐIỂM
    public void AddPlayer1Score()
    {
        int score = PlayerPrefs.GetInt(PLAYER1_SCORE_KEY, 0);
        PlayerPrefs.SetInt(PLAYER1_SCORE_KEY, score + 1);
    }

    public void AddPlayer2Score()
    {
        int score = PlayerPrefs.GetInt(PLAYER2_SCORE_KEY, 0);
        PlayerPrefs.SetInt(PLAYER2_SCORE_KEY, score + 1);
    }

    // 🔥 LẤY ĐIỂM
    public int GetPlayer1Score()
    {
        return PlayerPrefs.GetInt(PLAYER1_SCORE_KEY, 0);
    }

    public int GetPlayer2Score()
    {
        return PlayerPrefs.GetInt(PLAYER2_SCORE_KEY, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
