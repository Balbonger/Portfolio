using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    //public GameObject TutorialPanel;
    public Image original;
    public Sprite[] newSprite;

    public GameObject TutorialPanel;
    public GameObject originaltext;
    public string[] dialogue;
    int counter = 1;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if(PlayerPrefs.GetInt("TotalFin") != 0)
        {
            TutorialPanel.SetActive(true);
        }
        TutorialPanel.SetActive(false);
        */
        TutorialPanel.SetActive(true);
        TutorialCheck();
        Text text = originaltext.GetComponent<Text>();
        text.text = dialogue[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialCheck()
    {
        int totalfin = PlayerPrefs.GetInt("TotalReplays");
        if (totalfin != 0)
        {
            TutorialPanel.SetActive(false);
            Debug.Log("cutted");
        }
    }

    public void Spritechange()
    {
        original.sprite = newSprite[Random.Range(0, newSprite.Length)];
        
        if(dialogue.Length != counter)
        {
            Text text = originaltext.GetComponent<Text>();
            text.text = dialogue[counter];
            counter++;
        }else if (dialogue.Length+1 >= counter)
        {
            TutorialPanel.SetActive(false);
        }
           
        

        
    }
}
