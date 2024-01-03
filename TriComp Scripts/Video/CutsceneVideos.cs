using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneVideos : MonoBehaviour
{
    public AudioSource BGM;
    public GameObject Vidplayer1;
    public GameObject Vidplayer2;
    public GameObject Vidplayer3;
    public GameObject Vidplayer4;
    public GameObject Vidplayer5;
    public GameObject Vidplayer6;
    public GameObject Vidplayer7;
    public GameObject Vidplayer8;
    public GameObject Vidplayer9;

    public VideoPlayer renderplay1;
    public VideoPlayer renderplay2;
    public VideoPlayer renderplay3;
    public VideoPlayer renderplay4;
    public VideoPlayer renderplay5;
    public VideoPlayer renderplay6;
    public VideoPlayer renderplay7;
    public VideoPlayer renderplay8;
    public VideoPlayer renderplay9;

    public int duration1;
    public int duration2;
    public int duration3;
    public int duration4;
    public int duration5;
    public int duration6;
    public int duration7;
    public int duration8;
    public int duration9;

    public GameObject Cskip1;
    public GameObject Cskip2;
    public GameObject Cskip3;
    public GameObject Cskip4;
    public GameObject Cskip5;
    public GameObject Cskip6;
    public GameObject Cskip7;
    public GameObject Cskip8;
    public GameObject Cskip9;

    // Start is called before the first frame update
    void Start()
    {
        Vidplayer1.SetActive(false);
        Vidplayer2.SetActive(false);
        Vidplayer3.SetActive(false);
        Vidplayer4.SetActive(false);
        Vidplayer5.SetActive(false);
        Vidplayer6.SetActive(false);
        Vidplayer7.SetActive(false);
        Vidplayer8.SetActive(false);
        Vidplayer9.SetActive(false);

        renderplay1.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay2.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay3.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay4.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay5.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay6.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay7.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay8.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
        renderplay9.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));

    }

    public void play1()
    {
        BGM.Stop();
        Vidplayer1.SetActive(true);
        renderplay1.Play();
        StartCoroutine(Sandali1());
    }
    public void play2()
    {
        BGM.Stop();
        Vidplayer2.SetActive(true);
        renderplay2.Play();
        StartCoroutine(Sandali2());
    }
    public void play3()
    {
        BGM.Stop();
        Vidplayer3.SetActive(true);
        renderplay3.Play();
        StartCoroutine(Sandali3());
    }

    public void play4()
    {
        BGM.Stop();
        Vidplayer4.SetActive(true);
        renderplay4.Play();
        StartCoroutine(Sandali4());
    }
    public void play5()
    {
        BGM.Stop();
        Vidplayer5.SetActive(true);
        renderplay5.Play();
        StartCoroutine(Sandali5());
    }
    public void play6()
    {
        BGM.Stop();
        Vidplayer6.SetActive(true);
        renderplay6.Play();
        StartCoroutine(Sandali6());
    }
    public void play7()
    {
        BGM.Stop();
        Vidplayer7.SetActive(true);
        renderplay7.Play();
        StartCoroutine(Sandali7());
    }
    public void play8()
    {
        BGM.Stop();
        Vidplayer8.SetActive(true);
        renderplay8.Play();
        StartCoroutine(Sandali8());
    }
    public void play9()
    {
        BGM.Stop();
        Vidplayer9.SetActive(true);
        renderplay9.Play();
        StartCoroutine(Sandali9());
    }


    IEnumerator Sandali1()
    {
        yield return new WaitForSeconds(duration1);
        Vidplayer1.SetActive(false);
    }
    IEnumerator Sandali2()
    {
        yield return new WaitForSeconds(duration2);
        Vidplayer2.SetActive(false);
    }
    IEnumerator Sandali3()
    {
        yield return new WaitForSeconds(duration3);
        Vidplayer3.SetActive(false);
    }
    IEnumerator Sandali4()
    {
        yield return new WaitForSeconds(duration4);
        Vidplayer4.SetActive(false);
    }
    IEnumerator Sandali5()
    {
        yield return new WaitForSeconds(duration5);
        Vidplayer5.SetActive(false);
    }
    IEnumerator Sandali6()
    {
        yield return new WaitForSeconds(duration6);
        Vidplayer6.SetActive(false);
    }
    IEnumerator Sandali7()
    {
        yield return new WaitForSeconds(duration7);
        Vidplayer7.SetActive(false);
    }
    IEnumerator Sandali8()
    {
        yield return new WaitForSeconds(duration8);
        Vidplayer8.SetActive(false);
    }
    IEnumerator Sandali9()
    {
        yield return new WaitForSeconds(duration9);
        Vidplayer9.SetActive(false);
    }

    public void Skip1()
    {
        Time.timeScale = 0;
        Cskip1.SetActive(true);
        renderplay1.Pause();
    }

    public void ConfirmSkip1()
    {
        Time.timeScale = 1;
        Cskip1.SetActive(false);
        Vidplayer1.SetActive(false);
        renderplay1.Stop();
        BGM.Play();
    }

    public void Resume1()
    {
        Time.timeScale = 1;
        Cskip1.SetActive(false);
        renderplay1.Play();
    }

    public void Skip2()
    {
        Time.timeScale = 0;
        Cskip2.SetActive(true);
        renderplay2.Pause();
    }

    public void ConfirmSkip2()
    {
        Time.timeScale = 1;
        Cskip2.SetActive(false);
        Vidplayer2.SetActive(false);
        renderplay2.Stop();
        BGM.Play();
    }

    public void Resume2()
    {
        Time.timeScale = 1;
        Cskip2.SetActive(false);
        renderplay2.Play();
    }

    public void Skip3()
    {
        Time.timeScale = 0;
        Cskip3.SetActive(true);
        renderplay3.Pause();
    }

    public void ConfirmSkip3()
    {
        Time.timeScale = 1;
        Cskip3.SetActive(false);
        Vidplayer3.SetActive(false);
        renderplay3.Stop();
        BGM.Play();
    }

    public void Resume3()
    {
        Time.timeScale = 1;
        Cskip3.SetActive(false);
        renderplay3.Play();
    }

    public void Skip4()
    {
        Time.timeScale = 0;
        Cskip4.SetActive(true);
        renderplay4.Pause();
    }

    public void ConfirmSkip4()
    {
        Time.timeScale = 1;
        Cskip4.SetActive(false);
        Vidplayer4.SetActive(false);
        renderplay4.Stop();
        BGM.Play();
    }

    public void Resume4()
    {
        Time.timeScale = 1;
        Cskip4.SetActive(false);
        renderplay4.Play();
    }

    public void Skip5()
    {
        Time.timeScale = 0;
        Cskip5.SetActive(true);
        renderplay5.Pause();
    }

    public void ConfirmSkip5()
    {
        Time.timeScale = 1;
        Cskip5.SetActive(false);
        Vidplayer5.SetActive(false);
        renderplay5.Stop();
        BGM.Play();
    }

    public void Resume5()
    {
        Time.timeScale = 1;
        Cskip5.SetActive(false);
        renderplay5.Play();
    }

    public void Skip6()
    {
        Time.timeScale = 0;
        Cskip6.SetActive(true);
        renderplay6.Pause();
    }

    public void ConfirmSkip6()
    {
        Time.timeScale = 1;
        Cskip6.SetActive(false);
        Vidplayer6.SetActive(false);
        renderplay6.Stop();
        BGM.Play();
    }

    public void Resume6()
    {
        Time.timeScale = 1;
        Cskip6.SetActive(false);
        renderplay6.Play();
    }

    public void Skip7()
    {
        Time.timeScale = 0;
        Cskip7.SetActive(true);
        renderplay7.Pause();
    }

    public void ConfirmSkip7()
    {
        Time.timeScale = 1;
        Cskip7.SetActive(false);
        Vidplayer7.SetActive(false);
        renderplay7.Stop();
        BGM.Play();
    }

    public void Resume7()
    {
        Time.timeScale = 1;
        Cskip7.SetActive(false);
        renderplay7.Play();
    }

    public void Skip8()
    {
        Time.timeScale = 0;
        Cskip8.SetActive(true);
        renderplay8.Pause();
    }

    public void ConfirmSkip8()
    {
        Time.timeScale = 1;
        Cskip8.SetActive(false);
        Vidplayer8.SetActive(false);
        renderplay8.Stop();
        BGM.Play();
    }

    public void Resume8()
    {
        Time.timeScale = 1;
        Cskip8.SetActive(false);
        renderplay8.Play();
    }

    public void Skip9()
    {
        Time.timeScale = 0;
        Cskip9.SetActive(true);
        renderplay9.Pause();
    }

    public void ConfirmSkip9()
    {
        Time.timeScale = 1;
        Cskip9.SetActive(false);
        Vidplayer9.SetActive(false);
        renderplay9.Stop();
        BGM.Play();
    }

    public void Resume9()
    {
        Time.timeScale = 1;
        Cskip9.SetActive(false);
        renderplay9.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
