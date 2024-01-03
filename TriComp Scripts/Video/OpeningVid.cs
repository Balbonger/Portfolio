using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpeningVid : MonoBehaviour
{
    public AudioSource BGM;
    [SerializeField] private VideoPlayer videoPlayer;
    public GameObject intro;

    int gate;
    // Start is called before the first frame update
    void Start()
    {
        gate = PlayerPrefs.GetInt("Intro");
        PlayerPrefs.SetInt("Intro", 1);
        if (gate != 0)
        {
 
            videoPlayer.Stop();
            intro.SetActive(false);
            BGM.Play();
            //PlayerPrefs.SetInt("Intro", 1);
            Debug.Log(gate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
