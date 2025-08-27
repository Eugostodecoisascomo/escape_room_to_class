using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI captionsText;
    public GameObject cursor;
    public GameObject backButton;



    public Image iteractionImage;
    public TextMeshProUGUI assimVoceMeMata;


    public GameObject inventoryImage;
    public TextMeshProUGUI[] inventoryItens;
    public TextMeshProUGUI infoText;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryImage.SetActive(!inventoryImage.activeInHierarchy);
        }
    }

    public void SetCaptions(string text)
    {
        captionsText.text = text;
    }

    public void SetHandCursor(bool state)
    {
        cursor.SetActive(state);

        if(!state)
        {
            iteractionImage.enabled = false;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            backButton.SetActive(state);
        }
    }

    public void SetImage(Sprite sprite)
    {
        iteractionImage.sprite = sprite;
        iteractionImage.enabled = true;
    }

    public void SetItens(Item item , int index)
    {
        inventoryItens[index].text = item.collectMessage;
        infoText.text = item.collectMessage;
        StartCoroutine(FadingText());
    }
    IEnumerator FadingText()
    {
        Color newColor = infoText.color;
        while(newColor.a < 1)
        {
            newColor.a += Time.deltaTime;
            infoText.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds (2f);

        while(newColor.a > 0)
        {
            newColor.a -= Time.deltaTime;
            infoText.color = newColor;
            yield return null;
        }
    }
}
