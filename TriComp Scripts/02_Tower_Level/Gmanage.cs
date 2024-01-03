using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gmanage : MonoBehaviour
{
    public GameObject YouWinPanel;
    public GameObject enemy;
    public Transform SpawnEnemy;

    public AudioSource YouWinAudio;
    public AudioSource BGM;
    int SceneLoad;
    public int nextSceneLoad;

    public float timeleft;
    public bool TimerOn = false;
    IEnumerator Spawnen;


    // Start is called before the first frame update
    void Start()
    {
        SceneLoad = SceneManager.GetActiveScene().buildIndex;
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        Time.timeScale = 1f;
        Spawnen = spawn();
        StartCoroutine(Spawnen);
    }
    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (timeleft > 0)
            {
                timeleft -= Time.deltaTime;
                updateTimer(timeleft);
            }
            else
            {
                Debug.Log("time is up");
                timeleft = 0;
                TimerOn = false;

                Time.timeScale = 0f;

                BGM.Stop();
                YouWinAudio.Play();

                YouWinPanel.SetActive(true);
                int currentLevel = SceneManager.GetActiveScene().buildIndex;
                int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 4;

                if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
                {
                    PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
                }

                if (exclusicecurrentLevel >= PlayerPrefs.GetInt("ProcessorlevelsUnlocked"))
                {
                    PlayerPrefs.SetInt("ProcessorlevelsUnlocked", exclusicecurrentLevel);
                    PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);

                }

                Debug.Log("Cpu LEVEL " + PlayerPrefs.GetInt("ProcessorlevelsUnlocked") + " UNLOCKED");
            }
        }

    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);
    }

    IEnumerator spawn()
    {
            while (timeleft > 0)
            {
                float waitTime = Random.Range(0.5f, 2f);
                yield return new WaitForSeconds(waitTime);
                if (TimerOn)
                {
                    GameObject en = Instantiate(enemy, SpawnEnemy.position, Quaternion.identity);
                }

        }
            
    }

    public void GameOn()
    {
        Time.timeScale = 1f;
        TimerOn = true;
    }

    public void GameOff()
    {
        Time.timeScale = 0f;
    }
    public void Restart()
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
    
    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneLoad);

    }
}
