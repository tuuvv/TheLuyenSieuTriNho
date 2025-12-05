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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frontSR = front.GetComponent<SpriteRenderer>();
        frontIMG = front.GetComponent<Image>();
        front.SetActive(true);
        back.SetActive(false);
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
            // ShowEndGamePanel();
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
            // ShowEndGamePanel();
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
            // ShowEndGamePanel();
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

        // gamemanager.instance.AddPlayer1Score();
    }

    // Player 2 (consume + add score)
    public void NextLoad2()
    {
        // if (AudioSource != null) AudioSource.PlayOneShot(player2);

        ResetToFront();
        LoadRandomSpriteAndConsume();

        // gamemanager.instance.AddPlayer2Score();
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
