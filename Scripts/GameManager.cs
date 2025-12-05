using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    int score = 10;
    int lives = 3;
    int level = 1;
    int highScore = 0;
    int time = 0;
    int timeLimit = 60;
    public int hp = 10;
    float speed = 10.0f;

    int[] nums = { 1, 2, 3 };
    List<int> scores = new List<int>();
    List<string> names = new List<string>() { "A", "B", "C" };
    int x = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    string result = (score >= 50) ? "Đậu" : "Rớt";
    Debug.Log(result);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
