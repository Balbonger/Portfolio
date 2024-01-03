using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public int nextSceneLoad;
    int SceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneLoad = SceneManager.GetActiveScene().buildIndex;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapSelector");
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        //Debug.Log("PlayerPrefs Deleted");

    }

    public void QuitGame()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Restart()
    {
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(SceneLoad);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapSelector");
        }
        //Debug.Log("Clicked");
    }
    public void pause()
    {
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneLoad);

        /*
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
        }

        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelsUnlocked") + " UNLOCKED");
        */
    }

    public void cheat()
    {
        PlayerPrefs.SetInt("MobolevelsUnlocked", 5);
        PlayerPrefs.SetInt("ProcessorlevelsUnlocked", 5);
        PlayerPrefs.SetInt("RamlevelsUnlocked", 5);
        PlayerPrefs.SetInt("GpulevelsUnlocked", 5);
        PlayerPrefs.SetInt("InputlevelsUnlocked", 5);
        PlayerPrefs.SetInt("OutputlevelsUnlocked", 5);
        PlayerPrefs.SetInt("StoragelevelsUnlocked", 5);
        PlayerPrefs.SetInt("PsulevelsUnlocked", 5);
        PlayerPrefs.SetInt("TotalFin", 34);
    }

    public static bool isDirectAlmanac;
    public void alamanacDirect()
    {
        SceneManager.LoadScene("MapSelector");
        isDirectAlmanac = true;

    }

    public void rrest()
    {
        SceneManager.LoadScene(SceneLoad);
    }
}
