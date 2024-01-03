using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketFinish : MonoBehaviour
{
    public GameObject YouwinPanel;
    public AudioSource YouWinAudio;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        YouwinPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            YouwinPanel.SetActive(true);

            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 24;

            BGM.Stop();
            YouWinAudio.Play();

            Time.timeScale = 0f;

            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }
            if (exclusicecurrentLevel >= PlayerPrefs.GetInt("StoragelevelsUnlocked"))
            {
                PlayerPrefs.SetInt("StoragelevelsUnlocked", exclusicecurrentLevel);
                PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);
            }

            //Debug.Log("Storage Level " + PlayerPrefs.GetInt("StoragelevelsUnlocked") + " UNLOCKED");

        }
    }
}
