using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameInput : MonoBehaviour
{
    public InputField inputPlayer1;
    public InputField inputPlayer2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public void OnClickStartGame()
    {
        string p1 = inputPlayer1.text.Trim();
        string p2 = inputPlayer2.text.Trim();

        if (p1 == "") p1 = "Player 1";
        if (p2 == "") p2 = "Player 2";

        GameManager.instance.SetPlayerNames(p1, p2);

        // Load scene chứa CardFlip
        SceneManager.LoadScene("GamePlay");
    }
}
