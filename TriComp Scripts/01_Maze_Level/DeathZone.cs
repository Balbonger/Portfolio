using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public int nextSceneLoad;
    public GameObject YouFailPanel;
    public AudioSource YouFailAudio;
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex;
        YouFailPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(nextSceneLoad

            PlayerPrefs.SetInt("currentEnergy", PlayerPrefs.GetInt("currentEnergy") - 1);
            YouFailPanel.SetActive(true);
            YouFailAudio.Play();
            BGM.Stop();
        }
    }
}
