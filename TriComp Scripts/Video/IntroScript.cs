using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroScript : MonoBehaviour
{
    public int Duration;
    public AudioSource BGM;
    public VideoPlayer vid;
    void Start()
    {
        int rounds = 0;

        if (LevelSelection.StopIntro)
        {
            rounds = 1;
            vid.Stop();
            gameObject.SetActive(false);
            BGM.Play();
            LevelSelection.StopIntro = false;
        }

        if (rounds == 0)
        {
            StartCoroutine(Sandali());
            rounds = 1;
        }
    }

    IEnumerator Sandali()
    {
        yield return new WaitForSeconds(Duration);
        gameObject.SetActive(false);
        BGM.Play();
        
    }
}
