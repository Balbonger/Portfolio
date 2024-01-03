using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public int nextSceneLoad;
    [HideInInspector]public int SceneLoad;
    public GameObject YouWinPanel;
    public AudioSource BGM;
    public AudioSource YouWinAudio;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneLoad = SceneManager.GetActiveScene().buildIndex;
        Physics.gravity = new Vector3(0, -40f, 0);
        YouWinPanel.SetActive(false);

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneLoad);

    }

    public void RestartLevel()
    {
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(SceneLoad);
        }
        else
        {
            SceneManager.LoadScene("MapSelector");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(nextSceneLoad);

            YouWinPanel.SetActive(true);
            BGM.Stop();
            YouWinAudio.Play();

            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }

            if (currentLevel >= PlayerPrefs.GetInt("MobolevelsUnlocked"))
            {
                PlayerPrefs.SetInt("MobolevelsUnlocked", currentLevel );
                PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);

            }

            Debug.Log("Mobo LEVEL " + PlayerPrefs.GetInt("MobolevelsUnlocked") + " UNLOCKED");

            Physics.gravity = new Vector3(0, -9.8f, 0);

        }
    }
}
