using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl4Finish : MonoBehaviour
{
    public int nextSceneLoad;
    public GameObject FinishPanel;
    public AudioSource YouWinAudio;
    public AudioSource BGM;
    

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        FinishPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            FinishPanel.gameObject.SetActive(true);

            Physics.gravity = new Vector3(0, -9.8f, 0);

            BGM.Stop();
            YouWinAudio.Play();

            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 12;

            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }

            if (exclusicecurrentLevel >= PlayerPrefs.GetInt("GpulevelsUnlocked"))
            {
                PlayerPrefs.SetInt("GpulevelsUnlocked", exclusicecurrentLevel);
                PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);
            }

            //Debug.Log("Gpu LEVEL " + PlayerPrefs.GetInt("GpulevelsUnlocked") + " UNLOCKED");
            //Debug.Log("Total Replays = " + PlayerPrefs.GetInt("TotalReplays"));


        }
    }
}
