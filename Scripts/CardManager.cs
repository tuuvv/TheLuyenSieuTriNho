using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public float flipSpeed = 300f;
    private bool isFlipping = false;
    private bool showingFront = true;
    public Text cardNumber;
    public Text buttonNext1Text;
    public Text buttonNext2Text;
    private List<Sprite> spritePool = new List<Sprite>();
    private Sprite currentSprite;
    private SpriteRenderer frontSR;
    private Image frontIMG;
    public GameObject endGamePanel;
    public Button btnNext1;
    public Button btnNext2;

    public Text txtP1Name, txtP2Name, txtP1Score, txtP2Score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (endGamePanel != null)
            endGamePanel.SetActive(false);

        frontSR = front.GetComponent<SpriteRenderer>();
        frontIMG = front.GetComponent<Image>();
        front.SetActive(true);
        back.SetActive(false);
        if (buttonNext1Text != null)
            buttonNext1Text.text = GameManager.instance.player1Name;

        if (buttonNext2Text != null)
            buttonNext2Text.text = GameManager.instance.player2Name;
        LoadAllSprites();
        PeekRandomSprite();
    }

    void LoadAllSprites()
    {
        spritePool.Clear();
        Sprite[] loaded = Resources.LoadAll<Sprite>("Sprites");
        Debug.Log("total sprite" + loaded.Length);
        foreach (var sp in loaded)
        {
            int number;
            if (sp.name.Length == 2 && int.TryParse(sp.name, out number))
              { 
                spritePool.Add(sp);
                Debug.Log("item" + spritePool.Count);
              }
        }
    }
    void PeekRandomSprite()
    {
        Debug.Log("list sprite" + spritePool.Count);
        if (spritePool.Count == 0)
        {
            // nếu không còn thẻ, show EndGame
            ShowEndGamePanel();
            return;
        }

        int rand = Random.Range(0, spritePool.Count);
        currentSprite = spritePool[rand];

        if (frontSR != null)
            frontSR.sprite = currentSprite;
        else if (frontIMG != null)
            frontIMG.sprite = currentSprite;

        if (cardNumber != null)
            cardNumber.text = currentSprite.name;
    }
    void LoadRandomSpriteAndConsume()
    {
        if (spritePool.Count == 0)
        {
            ShowEndGamePanel();
            return;
        }

        int rand = Random.Range(0, spritePool.Count);
        currentSprite = spritePool[rand];
        spritePool.RemoveAt(rand); // consume

        if (frontSR != null)
            frontSR.sprite = currentSprite;
        else if (frontIMG != null)
            frontIMG.sprite = currentSprite;

        if (cardNumber != null)
            cardNumber.text = currentSprite.name;

        // nếu vừa remove khiến hết thẻ thì hiện panel EndGame luôn (tuỳ mong muốn)
        if (spritePool.Count == 0)
        {
            // gọi ShowEndGamePanel() ở đây nếu muốn hiện ngay khi rút hết.
            ShowEndGamePanel();
        }
    }
    void ResetToFront()
    {
        if (!showingFront)
        {
            showingFront = true;
            front.SetActive(true);
            back.SetActive(false);
            if (cardNumber != null) cardNumber.gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Player 1 (consume + add score)
    public void NextLoad()
    {
        // if (AudioSource != null) AudioSource.PlayOneShot(player1);

        ResetToFront();
        LoadRandomSpriteAndConsume();

        GameManager.instance.AddPlayer1Score();
    }

    // Player 2 (consume + add score)
    public void NextLoad2()
    {
        // if (AudioSource != null) AudioSource.PlayOneShot(player2);

        ResetToFront();
        LoadRandomSpriteAndConsume();

        GameManager.instance.AddPlayer2Score();
    }
    public void ShowEndGamePanel()
    {
        if (endGamePanel == null) return;

        endGamePanel.SetActive(true);

        // ❌ khóa nút để không bấm được nữa
        if (btnNext1 != null) btnNext1.interactable = false;
        if (btnNext2 != null) btnNext2.interactable = false;

        txtP1Name.text  = GameManager.instance.player1Name;
        txtP2Name.text  = GameManager.instance.player2Name;

        txtP1Score.text = GameManager.instance.GetPlayer1Score().ToString();
        txtP2Score.text = GameManager.instance.GetPlayer2Score().ToString();

        Debug.Log("Hiện panel End Game");
    }
    public void ExitGame()
    {
        ShowEndGamePanel();
    }

     // ⭐⭐⭐ NEW: BUTTON RESTART
    public void RestartGame()
    {
        PlayerPrefs.SetInt(GameManager.PLAYER1_SCORE_KEY, 0);
        PlayerPrefs.SetInt(GameManager.PLAYER2_SCORE_KEY, 0);

        SceneManager.LoadScene("GamePlay");
    }

    // ⭐⭐⭐ NEW: BUTTON MENU
    public void GoToMenu()
    {
        PlayerPrefs.SetInt(GameManager.PLAYER1_SCORE_KEY, 0);
        PlayerPrefs.SetInt(GameManager.PLAYER2_SCORE_KEY, 0);
        
        SceneManager.LoadScene("Menu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipCard(){
        if (!isFlipping)
            StartCoroutine(Flip());
    }

    IEnumerator Flip(){
        isFlipping = true;
        
        while (transform.rotation.eulerAngles.y < 90f ||
               transform.rotation.eulerAngles.y > 270f)
        {
            transform.Rotate(0, flipSpeed * Time.deltaTime, 0);
            yield return null;
        }
        showingFront = !showingFront;
        front.SetActive(showingFront);
        back.SetActive(!showingFront);
        if (!showingFront && cardNumber != null)
                    cardNumber.gameObject.SetActive(true);
                else if (showingFront && cardNumber != null)
                    cardNumber.gameObject.SetActive(false);

         while (transform.rotation.eulerAngles.y < 180f)
        {
            transform.Rotate(0, flipSpeed * Time.deltaTime, 0);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);

        isFlipping = false;
    }
}
