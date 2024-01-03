using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class LvlIntroScript : MonoBehaviour
{
    public int Duration;
    public GameObject Cskip;
    public AudioSource BGM;
    [SerializeField] private VideoPlayer videoPlayer;

    public bool CRT;
    void Start()
    {
        Cskip.SetActive(false);
        BGM.volume = PlayerPrefs.GetFloat("MusicVolume");

            StartCoroutine(Sandali());

    }

    IEnumerator Sandali()
    {
        CRT = true;
        yield return new WaitForSeconds(Duration);
        gameObject.SetActive(false);
        CRT = false;
        BGM.Play();
    }

    public void Skip()
    {
        Time.timeScale = 0;
        Cskip.SetActive(true);
        videoPlayer.Pause();
    }

    public void ConfirmSkip()
    {
        Time.timeScale = 1;
        Cskip.SetActive(false);
        gameObject.SetActive(false);
        videoPlayer.Stop();
        BGM.Play();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cskip.SetActive(false);
        videoPlayer.Play();
    }
}
