using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pieceScript : MonoBehaviour
{
    public Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    private bool sl;
    private bool nl;
    private static int count= 0;

    public GameObject YouWinPanel;

    public AudioSource YouWinAudio;
    public AudioSource BGM;
    public AudioSource PlusAudio;
    int nextSceneLoad;



    public int pieces;
    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3 (Random.Range(3, 8), Random.Range(4, -3));
        YouWinPanel.SetActive(false);
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {

            if (!Selected)
            {
                transform.position = RightPosition;
                if(!sl && transform.position == RightPosition)
                {
                    count++;
                    PlusAudio.Play();
                    //Debug.Log(count);
                    sl = true;
                }
                InRightPosition = true;
                
            }
            
        }

        if(!nl && pieces == count)
        {
            youwin();
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 20;


            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }
            if (exclusicecurrentLevel >= PlayerPrefs.GetInt("OutputlevelsUnlocked"))
            {
                PlayerPrefs.SetInt("OutputlevelsUnlocked", exclusicecurrentLevel);
                PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);
            }

            BGM.Stop();
            YouWinAudio.Play();
            //Debug.Log("Output Level " + PlayerPrefs.GetInt("OutputlevelsUnlocked") + " UNLOCKED");
            //Debug.Log("Total Replays = " + PlayerPrefs.GetInt("TotalReplays"));
            nl = true;
        }
    }

    public void youwin()
    {
        YouWinPanel.SetActive(true);
        Time.timeScale = 0;
        count = 0;
    }

}
