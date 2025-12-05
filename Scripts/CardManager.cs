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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
