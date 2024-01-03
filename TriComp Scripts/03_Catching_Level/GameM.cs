using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameM : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] private TextMesh lifeLabel;
    [SerializeField] private TextMesh DataLabel;
    public GameObject YouWinPanel;
    public GameObject YouFailPanel;
    [SerializeField] private float gravitySpeed;

    public AudioSource YouWinAudio;
    public AudioSource YouFailAudio;
    public AudioSource BGM;

    public AudioSource PlusAudio;
    public AudioSource MinusAudio;

    [SerializeField] private float minSpawn;
    [SerializeField] private float maxSpawn;
    [HideInInspector] public int count;
    public int Score;

    public int Lives;

    int sceneload;
    int nextsceneload;
    private bool ll;
    IEnumerator spawnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravitySpeed, 0);
        lifeLabel.text = "Lives Left: " + Lives;
        DataLabel.text = "Data Collected: " + count + " / " + Score;

        spawnEnemy = spawn();

        sceneload = SceneManager.GetActiveScene().buildIndex;
        nextsceneload = SceneManager.GetActiveScene().buildIndex + 1;
        YouWinPanel.SetActive(false);
        YouFailPanel.SetActive(false);

        //Time.timeScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!ll && Score == count)
        {
            UnlockLevel();
            ll = true;
        }

        if (!ll && Lives == 0)
        {
            PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
            Debug.Log("You lose");
            Time.timeScale = 0f;
            YouFailPanel?.SetActive(true);
            BGM.Stop();
            YouFailAudio.Play();
            ll = true;
        }
    }

    public void start()
    {
        Time.timeScale = 1f;
        StartCoroutine(spawnEnemy);
    }

    public void pause()
    {
        Time.timeScale = 0f;
    }

    public void unpause()
    {
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            SceneManager.LoadScene(sceneload);
        }
        else
        {
            SceneManager.LoadScene("MapSelector");
        }
    }

    public void nextlevel()
    {
        SceneManager.LoadScene(nextsceneload);
    }

    public int Increment()
    {
        count = count + 1;
        DataLabel.text = "Data Collected: " + count + " / " + Score;
        PlusAudio.Play();
        return count;
    }

    public int MinusLife()
    {
        Lives = Lives - 1;
        lifeLabel.text = "Lives Left: " + Lives;
        MinusAudio.Play();
        return Lives;
    }

    IEnumerator spawn()
    {
        while (count != Score)
        {
            float waitTime = Random.Range(minSpawn, maxSpawn);
            yield return new WaitForSeconds(waitTime);
            GameObject en = Instantiate(objects[Random.Range(0,objects.Length)], new Vector3(Random.Range(-8.25f, 8.33f), 6, 0), Quaternion.identity);

        }
    }

    public void UnlockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int exclusicecurrentLevel = SceneManager.GetActiveScene().buildIndex - 8;

        BGM.Stop();
        YouWinAudio.Play();

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
        }

        if (exclusicecurrentLevel >= PlayerPrefs.GetInt("RamlevelsUnlocked"))
        {
            PlayerPrefs.SetInt("RamlevelsUnlocked", exclusicecurrentLevel);
            PlayerPrefs.SetInt("TotalReplays", PlayerPrefs.GetInt("TotalReplays") + 1);
        }

        Debug.Log("Ram LEVEL " + PlayerPrefs.GetInt("RamlevelsUnlocked") + " UNLOCKED");
        Time.timeScale = 0f;

        YouWinPanel.SetActive(true);
    }
}
