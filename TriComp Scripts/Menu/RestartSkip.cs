using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RestartSkip : MonoBehaviour
{
    [HideInInspector]public int nextSceneLoad;
    public GameObject Intro;
    public VideoPlayer video;
    public AudioSource BGM;
    [HideInInspector] public static bool isSkip;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex;
        if (isSkip)
        {
            Intro.SetActive(false);
            video.Stop();
            BGM.Play();

            isSkip = false;
        }
    }

    public void RestartwithSkip()
    {
        if (PlayerPrefs.GetInt("currentEnergy") != 0)
        {
            isSkip = true;

            SceneManager.LoadScene(nextSceneLoad);
        }
        else
        {
            SceneManager.LoadScene("MapSelector");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
